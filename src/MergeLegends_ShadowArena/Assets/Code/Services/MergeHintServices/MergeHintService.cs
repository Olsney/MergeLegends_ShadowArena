using System;
using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;
using Code.Services.MergeUnits;
using Code.Units.Effects;
using Code.Units.PlayerUnits;
using Zenject;

namespace Code.Services.MergeHintServices
{
    public class MergeHintService : IInitializable, IDisposable, IMergeHintService
    {
        private readonly IMergeUnitsService _mergeService;
        private readonly IPlayerUnitsProvider _unitsProvider;
        private readonly List<PlayerUnit> _hinted = new();

        private PlayerUnit _selectedUnit;

        public MergeHintService(IMergeUnitsService mergeService, IPlayerUnitsProvider unitsProvider)
        {
            _mergeService = mergeService;
            _unitsProvider = unitsProvider;
        }

        public void Initialize()
            => _mergeService.SelectionChanged += OnSelectionChanged;

        public void Dispose()
        {
            _mergeService.SelectionChanged -= OnSelectionChanged;
            StopHints();
        }

        private void OnSelectionChanged(CellGridPlayerUnit selected)
        {
            StopHints();

            var selectedUnit = selected != null ? selected.Unit.GetComponent<PlayerUnit>() : null;

            if (selectedUnit == null)
                return;

            _selectedUnit = selectedUnit;
            _selectedUnit.Effects.Play(UnitEffectTypeId.Selected);

            foreach (PlayerUnit unit in _unitsProvider.GetPlayerUnits())
            {
                if (unit == null || unit == selectedUnit)
                    continue;

                if (unit.LevelIndex != selectedUnit.LevelIndex || unit.AttackType != selectedUnit.AttackType)
                    continue;

                unit.Effects.Play(UnitEffectTypeId.Merge);
                _hinted.Add(unit);
            }
        }

        private void StopHints()
        {
            if (_selectedUnit != null)
                _selectedUnit.Effects.Stop(UnitEffectTypeId.Selected);

            _selectedUnit = null;

            foreach (PlayerUnit unit in _hinted)
                if (unit != null)
                    unit.Effects.Stop(UnitEffectTypeId.Merge);

            _hinted.Clear();
        }
    }
}