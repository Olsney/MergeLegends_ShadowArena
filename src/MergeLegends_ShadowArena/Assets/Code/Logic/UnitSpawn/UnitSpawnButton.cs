using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.UnitSpawn
{
    public class UnitSpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IUnitSpawner _unitSpawner;

        [Inject]
        private void Construct(IUnitSpawner unitSpawner)
        {
            _unitSpawner = unitSpawner;
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
            _unitSpawner.Spawn();
        }
    }
}