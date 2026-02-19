using System;
using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class CellGridHero : MonoBehaviour
    {
        public GameObject Hero { get; private set; }
        public GameObject SelectedBorder { get; private set; }
        public bool IsOccupied { get; private set; }

        private void Awake()
        {
            SelectedBorder = GetComponentInChildren<BorderCellSelected>().gameObject;
            SelectedBorder.SetActive(false);
        }

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

        public void ActivateSelectedBorder()
        {
            SelectedBorder.SetActive(true);
        }

        public void DeactivateSelectedBorder()
        {
            SelectedBorder.SetActive(false);
        }
    }
}