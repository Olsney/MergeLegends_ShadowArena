using Code.Services.ReloadSceneService;

namespace Code.UI.ReloadScenes
{
    public class ReloadScenePresenter : Presenter<ReloadSceneView>
    {
        private readonly IReloadSceneService _reloadSceneService;

        public ReloadScenePresenter(ReloadSceneView view, IReloadSceneService reloadSceneService) : base(view)
        {
            _reloadSceneService = reloadSceneService;
        }

        public override void Initialize()
        {
            View.RestartLevelClicked += _reloadSceneService.StartReload;
        }

        public override void Dispose()
            => View.RestartLevelClicked -= _reloadSceneService.StartReload;
    }
}