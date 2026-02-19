using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGrid : MonoBehaviour
    {
        public GameObject Hero { get; private set; }
        public bool IsOccupied { get; private set; }

        public void PlaceUnit(GameObject unit)
        {
            Hero = unit;

            Hero.transform.position = transform.position;
            Hero.transform.SetParent(transform);

            IsOccupied = true;
        }

        public void RemoveUnit()
        {
            Hero.transform.SetParent(null);
            Hero = null;
            IsOccupied = false;
        }
    }
}