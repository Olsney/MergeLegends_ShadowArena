using System.Collections.Generic;
using Code.Signals;
using Code.Units;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;
using Zenject;

namespace Code.Logic.BattleServices
{
    public class BattleService : IBattleService
    {
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;
        private readonly SignalBus _signalBus;

        public BattleService(IPlayerUnitsProvider playerUnitsProvider, IEnemiesProvider enemiesProvider,
            SignalBus signalBus)
        {
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
            _signalBus = signalBus;
        }

        public void StartFight()
        {
            ProcessUnits(_playerUnitsProvider.GetUnits());
            ProcessUnits(_enemiesProvider.GetUnits());
            _signalBus.Fire(new StartFightSignal());
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