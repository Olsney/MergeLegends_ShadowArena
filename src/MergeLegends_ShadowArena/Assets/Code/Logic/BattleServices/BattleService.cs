using System.Collections.Generic;
using Code.Units;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;
using Zenject;

namespace Code.Logic
{
    public class BattleService : ITickable, IBattleService
    {
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;
        private bool _isFighting = false;

        public BattleService(IPlayerUnitsProvider playerUnitsProvider, IEnemiesProvider enemiesProvider)
        {
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
        }

        public void StartFight()
            => _isFighting = true;

        public void StopFight()
            => _isFighting = false;

        public bool CanStartFight()
            => _playerUnitsProvider.GetUnits().Count > 0;

        public void Tick()
        {
            if (_isFighting == false)
                return;

            ProcessUnits(_playerUnitsProvider.GetUnits());
            ProcessUnits(_enemiesProvider.GetUnits());
        }

        private void ProcessUnits(IReadOnlyList<IUnit> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null || units[i].IsDead)
                    continue;

                units[i].Tick();
            }
        }
    }
}