using Code.Infrastructure.Factory.Game;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.UI.Factory;

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
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IStaticDataService staticData,
            IPersistentProgressService progressService
        )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _staticData = staticData;
            _progressService = progressService;
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
            InformProgressReaders();

            _stateMachine.Enter<BattleStateMachine>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _uiFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
        
        private void InitGameWorld()
        {
            _uiFactory.CreateHud();
            _uiFactory.CreateSettingsPresenter();
            _uiFactory.CreateCurrencyPresenter();
            _uiFactory.CreateLevelPresenter();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();
    }
}