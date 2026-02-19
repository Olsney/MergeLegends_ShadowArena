using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.UnitSpawn
{
    public class UnitSpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IHeroSpawner heroSpawner;

        [Inject]
        private void Construct(IHeroSpawner heroSpawner)
        {
            this.heroSpawner = heroSpawner;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            heroSpawner.Spawn();
        }
    }
}