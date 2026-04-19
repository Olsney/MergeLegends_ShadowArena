using Code.Data;
using Code.Services.PersistentProgress;
using Code.UI.Factory;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IUIFactory _uiFactory;

        public SaveLoadService(IPersistentProgressService progressService, IUIFactory uiFactory)
        {
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _uiFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}