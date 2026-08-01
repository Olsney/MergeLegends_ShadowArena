using System;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.UnitInGridLogic;
using Code.Services.Audio;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factory;
using Code.Units.PlayerUnits;
using UnityEngine;
using Zenject;

namespace Code.Services.MergeUnits
{
    public class MergeUnitsService : IMergeUnitsService, IDisposable, IInitializable
    {
        private readonly IStaticDataService _staticData;
        private readonly IUnitBuilder _unitBuilder;
        private readonly IPlayerUnitGridSelectorService _selectorService;
        private readonly IPresenterFactory _uiFactory;
        private readonly IAudioService _audioService;

        private CellGridPlayerUnit _selectedCell;

        public MergeUnitsService(
            IUnitBuilder unitBuilder,
            IStaticDataService staticData,
            IPlayerUnitGridSelectorService selectorService,
            IPresenterFactory uiFactory,
            IAudioService audioService)
        {
            _unitBuilder = unitBuilder;
            _staticData = staticData;
            _selectorService = selectorService;
            _uiFactory = uiFactory;
            _audioService = audioService;
        }

        public void Initialize()
        {
            _selectorService.UnitSelected += OnUnitSelected;
            _selectorService.SelectionCleared += ClearSelection;
        }

        public void Dispose()
        {
            _selectorService.UnitSelected -= OnUnitSelected;
            _selectorService.SelectionCleared -= ClearSelection;
        }

        private void OnUnitSelected(CellGridPlayerUnit cell)
        {
            if (_selectedCell == null)
            {
                _selectedCell = cell;
                return;
            }

            if (_selectedCell == cell)
            {
                ClearSelection();
                return;
            }

            TryMerge(_selectedCell, cell);
            ClearSelection();

            _selectedCell = cell;
        }

        private void TryMerge(CellGridPlayerUnit cell1, CellGridPlayerUnit cell2)
        {
            var unit1 = cell1.Unit?.GetComponent<PlayerUnit>();
            var unit2 = cell2.Unit?.GetComponent<PlayerUnit>();

            if (unit1 == null || unit2 == null)
                return;

            if (unit1.LevelIndex != unit2.LevelIndex)
                return;

            if (unit1.AttackType != unit2.AttackType)
                return;

            int nextLevel = unit1.LevelIndex + 1;

            if (nextLevel >= _staticData.GetPlayerUnit(unit1.AttackType).UnitStatsByLevelIndexSetups.Count)
            {
                ShowMaxLevelToMergeMessage();
                return;
            }

            DestroyUnit(cell1);
            DestroyUnit(cell2);

            var newUnit = CreateNewLevelUpUnit(cell2, unit1, nextLevel);
            SetUnitInCell(cell2, newUnit);
            StartLevelUpEffect(newUnit);
        }

        private GameObject CreateNewLevelUpUnit(CellGridPlayerUnit cell2, PlayerUnit unit1, int nextLevel)
        {
            GameObject newUnit = _unitBuilder
                .SetUnitType(UnitTypeId.PlayerUnit)
                .SetUnitAttackType(unit1.AttackType)
                .SetCurrentLevel(nextLevel)
                .SetPosition(cell2.transform.position)
                .Build();

            _audioService.PlaySound(GameSoundTypeId.PlayerUnitMerge);

            return newUnit;
        }

        private static void SetUnitInCell(CellGridPlayerUnit cell2, GameObject newUnit)
            => cell2.SetUnit(newUnit);

        private void StartLevelUpEffect(GameObject newUnit)
            => newUnit.GetComponent<PlayerUnit>().StartLevelUpEffect();

        private void ShowMaxLevelToMergeMessage()
            => _uiFactory.CreateWarningMessagePresenter(
                _staticData.GetWarningMessage(WarningMessagesTypeId.MaxUnitLevel).Message);

        private void DestroyUnit(CellGridPlayerUnit cell)
        {
            var unit = cell.Unit;
            cell.RemoveUnit();
            unit.GetComponent<PlayerUnit>().Remove();
        }

        private void ClearSelection()
            => _selectedCell = null;
    }
}