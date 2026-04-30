using Code.Signals;
using Code.Units.UI.Texts;
using UnityEngine;
using Zenject;

namespace Code.Units.UI.Levels
{
    public class UnitLevelView : MonoBehaviour
    {
        private UnitLevelText _unitLevelText;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
            => _signalBus = signalBus;

        private void Awake()
            => _unitLevelText = GetComponentInChildren<UnitLevelText>();

        private void Start()
            => _signalBus.Subscribe<StartFightSignal>(Remove);

        private void OnDestroy()
            => _signalBus.Unsubscribe<StartFightSignal>(Remove);

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

        private void Remove()
            => Destroy(gameObject);
    }
}