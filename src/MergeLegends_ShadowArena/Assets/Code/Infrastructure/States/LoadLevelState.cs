using Code.Core.LoadingCurtains;
using Code.Services.PersistentProgress;
using Code.UI.Currency;
using Code.UI.Factory;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string EmptySceneName = "Empty";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;
        private readonly ILoadingCurtainProvider _loadingCurtainProvider;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IUIFactory uiFactory,
            IPresenterFactory presenterFactory,
            IPersistentProgressService progressService,
            ICurrencyPresenterProvider currencyPresenterProvider,
            ILoadingCurtainProvider loadingCurtainProvider
        )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _presenterFactory = presenterFactory;
            _progressService = progressService;
            _currencyPresenterProvider = currencyPresenterProvider;
            _loadingCurtainProvider = loadingCurtainProvider;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtainProvider.LoadingCurtain.Show();
            _sceneLoader.Load(EmptySceneName, () => _sceneLoader.Load(sceneName, OnLoaded));
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitTopLayerCanvas();
            InitGameWorld();
            InformProgressReaders();

            _loadingCurtainProvider.LoadingCurtain.Hide();
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
            _presenterFactory.CreateSettingsPresenter();
            CurrencyPresenter currencyPresenter = _presenterFactory.CreateCurrencyPresenter(_progressService);
            _presenterFactory.CreateLevelPresenter(_progressService);

            _currencyPresenterProvider.SetPresenter(currencyPresenter);
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitTopLayerCanvas()
            => _uiFactory.CreateTopLayerCanvas();
    }
}