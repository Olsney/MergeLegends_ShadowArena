using Code.UI.Texts;
using UnityEngine;

namespace Code.UI.Levels
{
    public class LevelView : MonoBehaviour, IView
    {
        private LevelText _levelText;

        private void Awake()
            => _levelText = GetComponentInChildren<LevelText>();

        public void SetValue(int levelNumber)
            => _levelText.TextMeshPro.text = $"Level {levelNumber}";
    }
}