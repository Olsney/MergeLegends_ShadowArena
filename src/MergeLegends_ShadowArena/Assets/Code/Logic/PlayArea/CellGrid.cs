using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class CellGrid : MonoBehaviour
    {
        private GameObject _occupyingUnit;
        private bool _isOccupied;

        public bool IsOccupied => _isOccupied;

        public void PlaceUnit(GameObject unit)
        {
            _occupyingUnit = unit;
            _isOccupied = true;
        }

        public void RemoveUnit()
        {
            if (_occupyingUnit != null)
                _occupyingUnit = null;
            
            _isOccupied = false;
        }
    }
}