using Code.Units.UI.Texts;
using UnityEngine;

namespace Code.Units.UI
{
    public class UnitLevelShower : MonoBehaviour
    {
        private UnitLevelText _unitLevelText;

        private void Awake() 
            => _unitLevelText = GetComponentInChildren<UnitLevelText>();

        public void Init(int levelIndex)
        {
            int levelNumber = levelIndex + 1;
            _unitLevelText.TextMeshPro.text = levelNumber .ToString();
        }
    }
}
