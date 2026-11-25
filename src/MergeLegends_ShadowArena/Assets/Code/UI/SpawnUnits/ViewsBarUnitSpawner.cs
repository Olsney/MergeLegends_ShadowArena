using UnityEngine;
using Zenject;

namespace Code.UI.SpawnUnits
{
    public class ViewsBarUnitSpawner : MonoBehaviour
    {
        [Inject]
        private void Construct(ISpawnButtonsParentProvider provider) 
            => provider.SetParent(transform);
    }
}