using System;
using System.Collections.Generic;
using Code.Logic.BattleResultShowers;
using Code.Units;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;

namespace Code.Logic.BattleServices
{
    public class BattleService : IBattleService
    {
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;
        private readonly IBattleResultService _battleResultService;

        private bool _battleEnded;

        public event Action FightStarting;

        public BattleService(IPlayerUnitsProvider playerUnitsProvider, IEnemiesProvider enemiesProvider,
            IBattleResultService battleResultService)
        {
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
            _battleResultService = battleResultService;
        }


        public void StartFight()
        {
            _battleEnded = false;

            FightStarting?.Invoke();

            ProcessUnits(_playerUnitsProvider.GetPlayerUnits());
            ProcessUnits(_enemiesProvider.GetEnemies());

            SubscribeToDeath(_playerUnitsProvider.GetPlayerUnits());
            SubscribeToDeath(_enemiesProvider.GetEnemies());
        }

        private void ProcessUnits<T>(IReadOnlyList<T> units) where T : IUnit
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null || units[i].IsDead)
                    continue;

                units[i].ProcessUnit();
            }
        }

        private void SubscribeToDeath<T>(IReadOnlyList<T> units) where T : IUnit
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].UnitDeath.Dead += OnUnitDead;
            }
        }

        private void OnUnitDead()
        {
            ValidateBattleState();
        }

        private void ValidateBattleState()
        {
            if (_battleEnded)
                return;

            int playersAlive = _playerUnitsProvider.GetPlayerUnits().Count;
            int enemiesAlive = _enemiesProvider.GetEnemies().Count;

            if (playersAlive == 0 || enemiesAlive == 0)
            {
                _battleEnded = true;

                if (playersAlive == 0)
                {
                    CompleteBattle(_enemiesProvider.GetEnemies());
                    _battleResultService.StartLossProcess();
                }
                else
                {
                    CompleteBattle(_playerUnitsProvider.GetPlayerUnits());
                    _battleResultService.StartVictoryProcess();
                }
            }
        }

        private void CompleteBattle<T>(IReadOnlyList<T> units) where T : IUnit
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null || units[i].IsDead)
                    continue;

                units[i].StartVictoryAnimation();
            }
        }
    }
}