using System;
using Code.Data;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.UnitInGridLogic;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.Tutorial;
using Code.StaticData.Tutorial;
using Code.UI.Factory;
using Code.UI.Fights;
using Code.UI.SpawnUnits;
using UnityEngine;

namespace Code.UI.Tutorial
{
    public class TutorialFlow : ITutorialFlow
    {
        private const float MoveSqrTolerance = 0.01f;

        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerUnitCellsProvider _playerUnitCellsProvider;
        private readonly IPlayerUnitGridSelectorService _playerUnitGridSelectorService;

        private TutorialStaticData _config;
        private SpotlightMask _spotlightMask;
        private bool _isRunning;

        private Action _stopCurrentStep = () => { };

        public TutorialFlow(
            IUIFactory uiFactory,
            IPersistentProgressService progressService,
            IStaticDataService staticDataService,
            IPlayerUnitCellsProvider playerUnitCellsProvider,
            IPlayerUnitGridSelectorService playerUnitGridSelectorService)
        {
            _uiFactory = uiFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _playerUnitCellsProvider = playerUnitCellsProvider;
            _playerUnitGridSelectorService = playerUnitGridSelectorService;
        }

        public void TryStart()
        {
            if (_progressService.Progress.WorldData.TutorialData.IntroCompleted)
                return;

            _config = _staticDataService.GetTutorialData();

            Debug.Assert(_config != null, "TutorialStaticData is null");

            _isRunning = true;
            _spotlightMask = _uiFactory.CreateTutorialOverlay().GetComponentInChildren<SpotlightMask>(true);

            HideAllGameplayButtons();
            StartBuyMeleeStep();
        }

        private void Stop()
        {
            if (_isRunning == false)
                return;

            _isRunning = false;

            _stopCurrentStep();
            _stopCurrentStep = () => { };

            if (_spotlightMask != null)
                _spotlightMask.Hide();
        }

        private void HideAllGameplayButtons()
        {
            GetHider(_uiFactory.GetView<MeleePlayerUnitPurchaseView>()).Hide();
            GetHider(_uiFactory.GetView<RangedPlayerUnitPurchaseView>()).Hide();
            GetHider(_uiFactory.GetView<StartFightView>()).Hide();
        }

        private TutorialButtonHiderAndShower GetHider(Component view)
        {
            if (view.TryGetComponent(out TutorialButtonHiderAndShower hider) == false)
                hider = view.gameObject.AddComponent<TutorialButtonHiderAndShower>();

            return hider;
        }

        private void StartBuyMeleeStep()
        {
            MeleePlayerUnitPurchaseView view = _uiFactory.GetView<MeleePlayerUnitPurchaseView>();

            StartPurchaseStep(
                view,
                handler => view.SpawnClicked += handler,
                handler => view.SpawnClicked -= handler,
                _config.MeleePurchasesRequired,
                TutorialPointerSide.Left,
                StartBuyRangedStep);
        }

        private void StartBuyRangedStep()
        {
            RangedPlayerUnitPurchaseView view = _uiFactory.GetView<RangedPlayerUnitPurchaseView>();

            StartPurchaseStep(
                view,
                handler => view.SpawnClicked += handler,
                handler => view.SpawnClicked -= handler,
                _config.RangedPurchasesRequired,
                TutorialPointerSide.Right,
                StartMergeUnitsStep);
        }

        private void StartPurchaseStep(Component view, Action<Action> subscribe, Action<Action> unsubscribe,
            int purchasesRequired, TutorialPointerSide pointerSide, Action onFinished)
        {
            GetHider(view).Show();

            int bought = 0;

            void OnBought()
            {
                if (++bought < purchasesRequired)
                    return;

                unsubscribe(OnBought);
                onFinished();
            }

            subscribe(OnBought);
            _stopCurrentStep = () => unsubscribe(OnBought);

            _spotlightMask.ShowUI(
                (RectTransform)view.transform,
                _config.PurchaseButtonsRadius,
                true,
                pointerSide);
        }

        private void StartMergeUnitsStep() =>
            StartCellSequenceStep(_config.MergeSteps, StartMoveUnitsStep);

        private void StartMoveUnitsStep() =>
            StartCellSequenceStep(_config.MoveSteps, StartFightStep, alsoListenForCellMove: true);

        private void StartCellSequenceStep(CellStep[] steps, Action onFinished, bool alsoListenForCellMove = false)
        {
            int step = 0;

            void Unsubscribe()
            {
                _playerUnitGridSelectorService.UnitSelected -= OnCellSelected;

                if (alsoListenForCellMove)
                    _playerUnitGridSelectorService.UnitCellChanged -= OnUnitMoved;
            }

            void ShowCurrent()
            {
                if (step >= steps.Length)
                {
                    Unsubscribe();
                    onFinished();
                    return;
                }

                CellGridPlayerUnit cell = GetPlayerCell(steps[step].CellIndex);

                if (cell != null)
                    _spotlightMask.ShowWorld(cell.Transform, _config.UnitsInCellRadius, true, steps[step].PointerSide);
            }

            void OnCellSelected(CellGridPlayerUnit cell)
            {
                if (cell.CellIndex != steps[step].CellIndex)
                    return;

                step++;
                ShowCurrent();
            }

            void OnUnitMoved(GameObject unit, Vector3 position)
            {
                CellGridPlayerUnit destination = GetPlayerCell(steps[step].CellIndex);

                if (destination == null || destination.transform.position.SqrMagnitudeTo(position) > MoveSqrTolerance)
                    return;

                step++;
                ShowCurrent();
            }

            _playerUnitGridSelectorService.UnitSelected += OnCellSelected;

            if (alsoListenForCellMove)
                _playerUnitGridSelectorService.UnitCellChanged += OnUnitMoved;

            _stopCurrentStep = Unsubscribe;

            ShowCurrent();
        }

        private void StartFightStep()
        {
            StartFightView view = _uiFactory.GetView<StartFightView>();
            GetHider(view).Show();

            void OnFightClicked()
            {
                view.FightClicked -= OnFightClicked;
                _spotlightMask.Hide();
                _progressService.Progress.WorldData.TutorialData.IntroCompleted = true;
                Stop();
            }

            view.FightClicked += OnFightClicked;
            _stopCurrentStep = () => view.FightClicked -= OnFightClicked;

            _spotlightMask.ShowUI((RectTransform)view.transform, _config.StartFightButtonRadius, true,
                TutorialPointerSide.Right);
        }

        private CellGridPlayerUnit GetPlayerCell(int index)
        {
            foreach (var cell in _playerUnitCellsProvider.GetAllCells())
            {
                if (cell is CellGridPlayerUnit playerCell && playerCell.CellIndex == index)
                    return playerCell;
            }

            return null;
        }
    }
}