using UnityEngine;

namespace Code.Units.UI
{
    public class UnitCanvas : MonoBehaviour
    {
        private UnitDeath _unitDeath;

        private void Awake()
            => _unitDeath = GetComponentInParent<UnitDeath>();

        private void Start()
            => _unitDeath.Dead += OnDisable;

        private void OnDestroy()
            => _unitDeath.Dead -= OnDisable;

        private void OnDisable()
            => gameObject.SetActive(false);
    }
}