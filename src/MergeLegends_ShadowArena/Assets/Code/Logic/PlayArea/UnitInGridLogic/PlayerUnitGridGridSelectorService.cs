using System;
using Code.Logic.PlayArea.Cell;
using Code.Services.Raycast;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.UnitInGridLogic
{
    public class PlayerUnitGridGridSelectorService : IPlayerUnitGridSelectorService, IDisposable, IInitializable
    {
        private readonly IRaycastService _raycastService;
        private CellGridPlayerUnit _selectedCell;

        public event Action<GameObject, Vector3> UntiCellChanged;

        public PlayerUnitGridGridSelectorService(IRaycastService raycastService)
        {
            _raycastService = raycastService;
        }

        public void Initialize()
            => _raycastService.HitPerformed += OnHitPerformed;

        public void Dispose()
            => _raycastService.HitPerformed -= OnHitPerformed;

        private void OnHitPerformed(RaycastHit hit)
        {
            if (hit.transform.gameObject.TryGetComponent<CellGridPlayerUnit>(out var cell))
            {
                HandleCell(cell);
                return;
            }

            ClearSelection();
        }

        private void HandleCell(CellGridPlayerUnit cell)
        {
            if (_selectedCell != null)
            {
                if (_selectedCell == cell)
                {
                    ClearSelection();
                    return;
                }

                if (cell.IsOccupied)
                {
                    ClearSelection();
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
            _selectedCell = cell;
            _selectedCell.ActivateSelectedBorder();
        }

        private void SetUnitInCell(CellGridPlayerUnit targetCell)
        {
            var playerUnit = _selectedCell.Unit;
            _selectedCell.RemoveUnit();
            targetCell.SetUnit(playerUnit);
            UntiCellChanged?.Invoke(playerUnit, targetCell.transform.position);
            ClearSelection();
        }

        private void ClearSelection()
        {
            if (_selectedCell != null)
            {
                _selectedCell.DeactivateSelectedBorder();
                _selectedCell = null;
            }
        }
    }
}