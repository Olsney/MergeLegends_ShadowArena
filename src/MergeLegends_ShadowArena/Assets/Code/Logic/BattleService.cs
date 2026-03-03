using Code.Units.PlayerUnit;
using Code.Units.PlayerUnits;
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
                if (units[i] == null || units[i].UnitAgentMover == null || units[i].Attacker  == null)
                    continue;
                
                units[i].UnitAgentMover.Tick();
                units[i].Attacker.Tick();
            }
        }
    }
}