using System;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.UnitInGridLogic
{
    public class PlayerUnitGridMoverService : IPlayerUnitGridMoverService, IInitializable, IDisposable
    {
        private readonly IPlayerUnitGridSelectorService _selectorService;

        public PlayerUnitGridMoverService(IPlayerUnitGridSelectorService selectorService)
        {
            _selectorService = selectorService;
        }

        public void Initialize()
            => _selectorService.UnitCellChanged += MoveUnitToCell;

        public void Dispose()
            => _selectorService.UnitCellChanged -= MoveUnitToCell;

        private void MoveUnitToCell(GameObject unit, Vector3 position)
            => unit.transform.position = position;
    }
}