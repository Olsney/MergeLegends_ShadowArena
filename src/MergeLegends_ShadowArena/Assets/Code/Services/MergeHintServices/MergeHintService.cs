using System;
using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;
using Code.Services.MergeUnits;
using Code.Units.PlayerUnits;
using Zenject;

namespace Code.Services.MergeHintServices
{
    public class MergeHintService : IInitializable, IDisposable, IMergeHintService
    {
        private readonly IMergeUnitsService _mergeService;
        private readonly IPlayerUnitsProvider _unitsProvider;
        private readonly List<PlayerUnit> _hinted = new ();

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

            foreach (PlayerUnit unit in _unitsProvider.GetPlayerUnits())
            {
                if (unit == null || unit == selectedUnit)
                    continue;

                if (unit.LevelIndex != selectedUnit.LevelIndex || unit.AttackType != selectedUnit.AttackType)
                    continue;
                
                unit.StartMergeHintEffect();
                _hinted.Add(unit);
            }
        }

        private void StopHints()
        {
            foreach (PlayerUnit unit in _hinted)
                if (unit != null)
                    unit.StopMergeHintEffect();

            _hinted.Clear();
        }
    }
}