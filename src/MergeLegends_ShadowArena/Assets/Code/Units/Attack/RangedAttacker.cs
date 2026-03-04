using Code.Infrastructure.Factory.Game;
using Code.Logic;
using Code.StaticData;
using Code.Units.MagicSphereAttack;
using UnityEngine;
using Zenject;

namespace Code.Units.Attack
{
    public class RangedAttacker : BaseAttacker
    {
        private IGameFactory _gameFactory;
        private MagicSphere _magicSphere;

        [Inject]
        private void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;

        protected override void OnAwake()
        {
            base.OnAwake();
            _magicSphere = _gameFactory.CreateMagicSpherePlayerUnit(transform.position).GetComponent<MagicSphere>();
        }

        public override void InitConfig(float damage, float cooldown, LayerMaskTypeId targetLayerMask)
        {
            _attackCooldown = cooldown;
            _magicSphere.Init(damage, targetLayerMask);
        }

        public override void Tick()
        {
            base.Tick();
            _magicSphere.Tick();
        }

        protected override void OnAttack()
        {
            if (_agentMover.GetCurrentTargetTransform() == null)
                return;
            
            _magicSphere.Launch(transform.position, _agentMover.GetCurrentTargetTransform());
        }
    }
}