using Code.Infrastructure.Factory.Game;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factory;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string EmptySceneName = "Empty";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IStaticDataService staticData
        )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(EmptySceneName, () => _sceneLoader.Load(sceneName, OnLoaded));
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InitPlayZone();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld() =>
            _uiFactory.CreateHud();

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitPlayZone()
        {
            LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);

            _gameFactory.CreatePlayZone(levelData.HeroesGridPlacesPosition, levelData.EnemiesGridPlacesPosition);
        }
    }
}