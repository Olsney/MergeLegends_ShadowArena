using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGridHero : MonoBehaviour, ICellGrid
    {
        public GameObject Unit { get; private set; }
        public bool IsOccupied { get; private set; }
        public GameObject SelectedBorder { get; private set; }

        private void Awake()
        {
            SelectedBorder = GetComponentInChildren<BorderCellSelected>().gameObject;
            SelectedBorder.SetActive(false);
        }

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
        
        public void ActivateSelectedBorder()
            => SelectedBorder.SetActive(true);

        public void DeactivateSelectedBorder()
            => SelectedBorder.SetActive(false);
    }
}