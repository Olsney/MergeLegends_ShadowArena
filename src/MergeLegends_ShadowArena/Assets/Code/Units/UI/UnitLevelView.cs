using Code.UI;
using Code.Units.UI.Texts;
using UnityEngine;

namespace Code.Units.UI
{
    public class UnitLevelView : MonoBehaviour, IView
    {
        private UnitLevelText _unitLevelText;

        private void Awake() 
            => _unitLevelText = GetComponentInChildren<UnitLevelText>();

        public void SetValue(int levelIndex)
        {
            int levelNumber = levelIndex + 1;
            _unitLevelText.TextMeshPro.text = levelNumber .ToString();
        }
    }
}