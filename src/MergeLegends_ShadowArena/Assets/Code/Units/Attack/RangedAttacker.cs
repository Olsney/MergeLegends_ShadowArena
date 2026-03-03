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
            => _magicSphere.Launch(transform.position, transform.forward);
    }
}