using Code.Infrastructure.Factory.Game;
using Code.Services.Input;
using Code.UI.Factory;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string EmptySceneName = "Empty";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IInputService _inputService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IInputService inputService
        )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _inputService = inputService;
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

            _inputService.InitCamera(Camera.main);
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld() =>
            _uiFactory.CreateHud();

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();
    }
}