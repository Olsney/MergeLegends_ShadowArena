using Code.Infrastructure.Factory.Game;
using Code.Infrastructure.Factory.Units;
using Code.UI.Factory;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;
        private IUnitFactory _unitFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory, IUIFactory uiFactory,  IUnitFactory unitFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _unitFactory = unitFactory;
        }

        public override void InstallBindings()
        {
            _gameFactory.SetSceneContainer(Container);
            _uiFactory.SetSceneContainer(Container);
            _unitFactory.SetSceneContainer(Container);
        }
    }
}