using Code.Units.PlayerUnit;
using Zenject;

namespace Code.Logic
{
    public class BattleService : ITickable, IBattleService
    {
        private readonly IPlayerUnitProvider _playerUnitProvider;
        //private readonly IEnemyProvider _enemyProvider;
        private bool _isActive = false;

        public BattleService(IPlayerUnitProvider playerUnitProvider)
        {
            _playerUnitProvider = playerUnitProvider;
        }

        public void StartBattle() 
            => _isActive = true;

        public void StopBattle() 
            => _isActive = false;

        public void Tick()
        {
            // if (_isActive == false)
            //     return;
        
            foreach (var unit in _playerUnitProvider.GetUnits())
            {
                if(unit == null || unit.UnitAgent == null)
                    continue;
            
                unit.UnitAgent.Move();
            }
        }
    }
}