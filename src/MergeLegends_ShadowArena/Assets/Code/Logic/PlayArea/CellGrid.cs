using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class CellGrid : MonoBehaviour
    {
        private GameObject _occupyingUnit;
        private bool _isOccupied;

        public bool IsOccupied => _isOccupied;

        public bool TryPlaceUnit(GameObject unit)
        {
            if (_isOccupied)
                return false;

            _occupyingUnit = unit;
            _isOccupied = true;

            if (unit != null)
            {
                unit.transform.position = transform.position + Vector3.up * 0.5f;
                unit.transform.SetParent(transform);
            }

            return true;
        }

        public void RemoveUnit()
        {
            if (_occupyingUnit != null)
            {
                _occupyingUnit.transform.SetParent(null);
                _occupyingUnit = null;
            }

            _isOccupied = false;
        }
    }
}