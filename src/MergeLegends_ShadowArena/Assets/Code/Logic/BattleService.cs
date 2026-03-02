using Code.Units.PlayerUnit;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class BattleService : ITickable, IBattleService
    {
        private readonly IPlayerUnitProvider _playerUnitProvider;
        private bool _isFighting = false;

        public BattleService(IPlayerUnitProvider playerUnitProvider)
        {
            _playerUnitProvider = playerUnitProvider;
        }

        public void StartFight()
            => _isFighting = true;

        public void StopFight()
            => _isFighting = false;

        public void Tick()
        {
            if (_isFighting == false)
                return;
            
            var units = _playerUnitProvider.GetUnits();
            
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null || units[i].UnitAgentMover == null)
                    continue;
                
                units[i].UnitAgentMover.Move();
            }
        }
    }
}