using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGridPlayerUnit : MonoBehaviour, ICellGrid
    {
        public Transform Transform => transform;
        public GameObject Unit { get; private set; }
        public bool IsOccupied { get; private set; }
        public GameObject SelectedBorder { get; private set; }

        private void Awake()
        {
            SelectedBorder = GetComponentInChildren<BorderCellSelected>().gameObject;
            SelectedBorder.SetActive(false);
        }

        public void SetUnit(GameObject unit)
        {
            Unit = unit;
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