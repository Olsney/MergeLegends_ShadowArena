using UnityEngine;

namespace Code.UI.SpawnUnits
{
    public class UnitSpawnerViewsRoot : MonoBehaviour
    {
        public void Remove()
            => Destroy(gameObject);
    }
}