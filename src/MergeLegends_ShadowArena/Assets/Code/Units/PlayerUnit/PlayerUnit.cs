using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnit
{
    public class PlayerUnit : MonoBehaviour
    {
        private IPlayerUnitProvider _playerUnitProvider;

        public PlayerUnitAgentMover UnitAgentMover { get; private set; }

        [Inject]
        private void Construct(IPlayerUnitProvider playerUnitProvider) 
            => _playerUnitProvider = playerUnitProvider;

        private void Awake()
        {
            UnitAgentMover = GetComponent<PlayerUnitAgentMover>();
            _playerUnitProvider.AddUnit(this);
        }

        private void OnDestroy() 
            => _playerUnitProvider.RemoveUnit(this);
    }
}