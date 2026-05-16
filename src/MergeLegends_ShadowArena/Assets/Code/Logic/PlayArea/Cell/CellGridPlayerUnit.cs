using System;
using System.Linq;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.Units.PlayerUnits;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGridPlayerUnit : MonoBehaviour, ICellGrid
    {
        public Transform Transform => transform;
        public GameObject Unit { get; private set; }
        public bool IsOccupied { get; private set; }
        public int CellIndex { get; private set; }

        private GameObject _selectedBorder;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _selectedBorder = GetComponentInChildren<BorderCellSelected>().gameObject;
            _selectedBorder.SetActive(false);
        }

        public void Init(int index, GameObject unit = null)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Cell index cannot be negative");

            CellIndex = index;

            if (unit != null)
            {
                Unit = unit;
                IsOccupied = true;
            }
        }

        public void SetUnit(GameObject unit)
        {
            Unit = unit;
            IsOccupied = true;

            SaveUnitInCell();
        }

        public void RemoveUnit()
        {
            RemoveSaveUnitInCell();

            Unit.transform.SetParent(null);
            Unit = null;
            IsOccupied = false;
        }

        public void ActivateSelectedBorder()
            => _selectedBorder.SetActive(true);

        public void DeactivateSelectedBorder()
            => _selectedBorder.SetActive(false);

        private void SaveUnitInCell()
        {
            PlayerUnitStatsInCellData playerUnitStatsInCellData = new PlayerUnitStatsInCellData();
            PlayerUnit playerUnit = Unit.GetComponent<PlayerUnit>();

            playerUnitStatsInCellData.CellIndex = CellIndex;
            playerUnitStatsInCellData.UnitAttackTypeId = playerUnit.AttackType;
            playerUnitStatsInCellData.PlayerUnitLevelIndex = playerUnit.LevelIndex;

            _persistentProgressService.Progress.WorldData
                .CompositionTeamData.PlayerUnitInCells.Add(playerUnitStatsInCellData);
            _saveLoadService.SaveProgress();
        }

        private void RemoveSaveUnitInCell()
        {
            var savedData = _persistentProgressService.Progress.WorldData
                .CompositionTeamData.PlayerUnitInCells
                .FirstOrDefault(data => data.CellIndex == CellIndex);

            _persistentProgressService.Progress.WorldData
                .CompositionTeamData.PlayerUnitInCells.Remove(savedData);
        }
    }
}