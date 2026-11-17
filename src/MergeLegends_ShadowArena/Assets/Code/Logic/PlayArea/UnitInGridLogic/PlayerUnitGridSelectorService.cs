using System;
using Code.Logic.PlayArea.Cell;
using Code.Services.Raycast;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.UnitInGridLogic
{
    public class PlayerUnitGridSelectorService : IPlayerUnitGridSelectorService, IDisposable, IInitializable
    {
        private readonly IRaycastService _raycastService;
        private CellGridPlayerUnit _selectedCell;

        public event Action<GameObject, Vector3> UnitCellChanged;
        public event Action<CellGridPlayerUnit> UnitSelected;
        public event Action SelectionCleared;

        public PlayerUnitGridSelectorService(IRaycastService raycastService)
        {
            _raycastService = raycastService;
        }

        public void Initialize()
        {
            _raycastService.HitPerformed += OnHitPerformed;
            _raycastService.EmptyHit += OnHitEmpty;
        }

        public void Dispose()
        {
            _raycastService.HitPerformed -= OnHitPerformed;
            _raycastService.EmptyHit -= OnHitEmpty;
        }
        
        public void ClearSelection()
        {
            _selectedCell = null;
            SelectionCleared?.Invoke();
        }

        private void OnHitEmpty()
        {
            SelectionCleared?.Invoke();
        }

        private void OnHitPerformed(RaycastHit hit)
        {
            if (hit.transform.gameObject.TryGetComponent<CellGridPlayerUnit>(out var cell))
            {
                HandleCell(cell);
                return;
            }

            SelectionCleared?.Invoke();
        }

        private void HandleCell(CellGridPlayerUnit cell)
        {
            if (_selectedCell != null)
            {
                if (_selectedCell == cell)
                {
                    SelectionCleared?.Invoke();
                    return;
                }

                if (cell.IsOccupied)
                {
                    SelectCell(cell);
                    return;
                }

                if (cell.IsOccupied == false)
                {
                    SetUnitInCell(cell);
                    return;
                }
            }

            if (cell.IsOccupied)
            {
                SelectCell(cell);
            }
        }

        private void SelectCell(CellGridPlayerUnit cell)
        {
            UnitSelected?.Invoke(cell);
            _selectedCell = cell;
        }

        private void SetUnitInCell(CellGridPlayerUnit targetCell)
        {
            var playerUnit = _selectedCell.Unit;
            _selectedCell.RemoveUnit();
            targetCell.SetUnit(playerUnit);
            UnitCellChanged?.Invoke(playerUnit, targetCell.transform.position);
            SelectionCleared?.Invoke();
        }
    }
}