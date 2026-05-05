using System;
using System.Collections.Generic;
using Code.Units;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;

namespace Code.Logic.BattleServices
{
    public class BattleService : IBattleService
    {
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;

        public event Action FightStarting;

        public BattleService(IPlayerUnitsProvider playerUnitsProvider, IEnemiesProvider enemiesProvider)
        {
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
        }

        public void StartFight()
        {
            FightStarting?.Invoke();
            ProcessUnits(_playerUnitsProvider.GetUnits());
            ProcessUnits(_enemiesProvider.GetUnits());
        }

        public bool CanStartFight()
            => _playerUnitsProvider.GetUnits().Count > 0;

        private void ProcessUnits(IReadOnlyList<IUnit> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null || units[i].IsDead)
                    continue;

                units[i].ProcessUnit();
            }
        }
    }
}