using System;
using Code.Infrastructure.Factory.Units;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.UnitInGridLogic;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factory;
using Code.Units.PlayerUnits;
using Zenject;

namespace Code.Services.MergeUnits
{
    public class MergeUnitsService : IMergeUnitsService, IDisposable, IInitializable
    {
        private readonly IStaticDataService _staticData;
        private readonly IUnitFactory _unitFactory;
        private readonly IPlayerUnitGridSelectorService _selectorService;
        private readonly IUIFactory _uiFactory;

        private CellGridPlayerUnit _selectedCell;

        public MergeUnitsService(
            IUnitFactory unitFactory,
            IStaticDataService staticData,
            IPlayerUnitGridSelectorService selectorService,
            IUIFactory uiFactory)
        {
            _unitFactory = unitFactory;
            _staticData = staticData;
            _selectorService = selectorService;
            _uiFactory = uiFactory;
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

            if (_staticData.GetPlayerUnit(unit1.AttackType).GetStatsLvl(nextLevel) == null)
            {
                ShowMaxLevelToMergeMessage();
                return;
            }

            DestroyUnit(cell1);
            DestroyUnit(cell2);

            var newUnit = _unitFactory.CreateUnit(
                UnitTypeId.PlayerUnit,
                unit1.AttackType,
                nextLevel,
                cell2.transform.position
            );

            cell2.SetUnit(newUnit);
            newUnit.GetComponent<PlayerUnit>().StartLevelUpEffect();
        }

        private void ShowMaxLevelToMergeMessage()
            => _uiFactory.CreateWarningMessage(
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