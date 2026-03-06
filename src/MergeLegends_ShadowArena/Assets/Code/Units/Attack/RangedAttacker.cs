using Code.Infrastructure.Factory.Game;
using Code.StaticData;
using Code.Units.MagicSphereAttack;
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

        public void InitConfig(float damage, float cooldown, LayerMaskTypeId targetLayerMask, float magicSphereSpeed)
        {
            _attackCooldown = cooldown;
            _magicSphere.Init(damage, targetLayerMask, magicSphereSpeed);
        }

        public override void Tick()
        {
            base.Tick();
            _magicSphere.Tick();
        }

        protected override void OnAttack()
        {
            if (NavmeshAgentMover.GetCurrentTargetTransform() == null)
                return;
            
            _magicSphere.Launch(transform.position, NavmeshAgentMover.GetCurrentTargetTransform());
        }
    }
}