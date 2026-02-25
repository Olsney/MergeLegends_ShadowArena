using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnit
{
    public class PlayerUnit : MonoBehaviour
    {
        private IPlayerUnitProvider _playerUnitProvider;

        public PlayerUnitAgent UnitAgent { get; private set; }

        [Inject]
        private void Construct(IPlayerUnitProvider playerUnitProvider) 
            => _playerUnitProvider = playerUnitProvider;

        private void Awake()
        {
            UnitAgent = GetComponent<PlayerUnitAgent>();
            _playerUnitProvider.AddUnit(this);
        }
    }
}