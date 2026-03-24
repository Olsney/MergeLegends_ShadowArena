using UnityEngine;

namespace Code.UI.SpawnUnits
{
    public class UnitSpawnerViewsRoot : MonoBehaviour
    {
        private ViewsBarUnitSpawner _viewsBarUnitSpawner;

        private void Awake() => 
            _viewsBarUnitSpawner = GetComponentInChildren<ViewsBarUnitSpawner>();

        public Transform GetViewsBarTransform()
            => _viewsBarUnitSpawner.transform;

        public void Remove()
            => Destroy(gameObject);
    }
}