using Code.Services.PersistentProgress;
using Code.UI.Texts;
using UnityEngine;
using Zenject;

namespace Code.UI.Levels
{
    public class LevelShower : MonoBehaviour
    {
        private IPersistentProgressService _progressService;
        private LevelText _levelText;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
            => _progressService = progressService;

        private void Awake()
            => _levelText = GetComponentInChildren<LevelText>();

        private void Start()
        {
            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            int levelNumber = levelIndex + 1;
            _levelText.TextMeshPro.text = "Level " + levelNumber;
        }
    }
}