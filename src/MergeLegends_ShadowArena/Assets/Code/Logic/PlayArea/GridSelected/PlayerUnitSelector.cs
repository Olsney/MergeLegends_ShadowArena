using System;
using Code.Logic.PlayArea.Cell;
using Code.Services.Raycast;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.GridSelected
{
    public class PlayerUnitSelector : IPlayerUnitSelector, IDisposable, IInitializable
    {
        private readonly IRaycastService _raycastService;
        private CellGridPlayerUnit _selectedCell;

        public PlayerUnitSelector(IRaycastService raycastService)
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
                    MoveUnitToCell(cell);
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

        private void MoveUnitToCell(CellGridPlayerUnit targetCell)
        {
            var playerUnit = _selectedCell.Unit;
            _selectedCell.RemoveUnit();
            targetCell.PlaceUnit(playerUnit);
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