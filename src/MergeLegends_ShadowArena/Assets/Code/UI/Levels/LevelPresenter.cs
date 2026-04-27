using Code.Services.PersistentProgress;

namespace Code.UI.Levels
{
    public class LevelPresenter : Presenter<LevelView>
    {
        private readonly IPersistentProgressService _progressService;

        public LevelPresenter(LevelView view, IPersistentProgressService progressService) : base(view)
        {
            _progressService = progressService;
        }

        public override void Initialize()
        {
            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            int levelNumber = levelIndex + 1;
            View.SetValue(levelNumber);
        }
    }
}