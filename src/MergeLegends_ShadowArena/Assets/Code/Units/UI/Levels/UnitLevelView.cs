using Code.UI;
using Code.Units.UI.Texts;
using UnityEngine;

namespace Code.Units.UI.Levels
{
    public class UnitLevelView : MonoBehaviour, IView
    {
        private UnitLevelText _unitLevelText;

        private void Awake()
            => _unitLevelText = GetComponentInChildren<UnitLevelText>();

        public void SetValue(int levelIndex)
        {
            if (levelIndex < 0)
            {
                Debug.LogError($"{nameof(levelIndex)} should be greater than or equal to 0.");
                return;
            }

            int levelNumber = levelIndex + 1;
            _unitLevelText.TextMeshPro.text = levelNumber.ToString();
        }

        public void Remove()
            => Destroy(gameObject);
    }
}