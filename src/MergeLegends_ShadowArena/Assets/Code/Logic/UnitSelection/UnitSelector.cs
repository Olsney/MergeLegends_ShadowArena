using System;
using Code.Logic.PlayArea;
using Code.Services.Raycast;
using UnityEngine;
using Zenject;

namespace Code.Logic.UnitSelection
{
    public class UnitSelector : IUnitSelector, IDisposable, IInitializable
    {
        private readonly IRaycastService _raycastService;
        private CellGridHero _selectedCell;

        public UnitSelector(IRaycastService raycastService)
        {
            _raycastService = raycastService;
        }

        public void Initialize()
            => _raycastService.HitPerformed += OnHitPerformed;

        public void Dispose()
            => _raycastService.HitPerformed -= OnHitPerformed;

        private void OnHitPerformed(RaycastHit hit)
        {
            if (hit.transform.gameObject.TryGetComponent<CellGridHero>(out var cell))
            {
                HandleCell(cell);
                return;
            }

            ClearSelection();
        }

        private void HandleCell(CellGridHero cell)
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

        private void SelectCell(CellGridHero cell)
        {
            _selectedCell = cell;
            _selectedCell.ActivateSelectedBorder();
        }

        private void MoveUnitToCell(CellGridHero targetCell)
        {
            var hero = _selectedCell.Hero;
            _selectedCell.RemoveUnit();
            targetCell.PlaceUnit(hero);
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