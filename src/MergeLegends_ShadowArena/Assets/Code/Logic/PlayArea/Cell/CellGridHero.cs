using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGridHero : CellGrid
    {
        public GameObject SelectedBorder { get; private set; }

        private void Awake()
        {
            SelectedBorder = GetComponentInChildren<BorderCellSelected>().gameObject;
            SelectedBorder.SetActive(false);
        }

        public void ActivateSelectedBorder()
            => SelectedBorder.SetActive(true);

        public void DeactivateSelectedBorder()
            => SelectedBorder.SetActive(false);
    }
}