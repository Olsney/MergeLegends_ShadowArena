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
        private CellGrid _selectedCell;

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
            if (hit.transform.gameObject.TryGetComponent<CellGrid>(out var cell) == false)
            {
                _selectedCell = null;
                return;
            }

            if (TrySelectCell(cell))
                return;

            if (_selectedCell != null && !cell.IsOccupied)
                MoveUnitToCell(cell);
            else
                _selectedCell = null;
        }

        private bool TrySelectCell(CellGrid cell)
        {
            if (_selectedCell != null)
                return false;

            if (cell.IsOccupied)
            {
                _selectedCell = cell;
                return true;
            }

            return false;
        }

        private void MoveUnitToCell(CellGrid targetCell)
        {
            var hero = _selectedCell.Hero;
            _selectedCell.RemoveUnit();
            targetCell.PlaceUnit(hero);
            _selectedCell = null;
        }
    }
}