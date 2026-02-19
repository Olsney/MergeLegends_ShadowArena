using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGrid : MonoBehaviour
    {
        public GameObject Unit { get; private set; }
        public bool IsOccupied { get; private set; }

        public void PlaceUnit(GameObject unit)
        {
            Unit = unit;

            Unit.transform.position = transform.position;
            Unit.transform.SetParent(transform);

            IsOccupied = true;
        }

        public void RemoveUnit()
        {
            Unit.transform.SetParent(null);
            Unit = null;
            IsOccupied = false;
        }
    }
}