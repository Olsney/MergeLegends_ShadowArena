using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class PlayZone : MonoBehaviour
    {
        public void Remove() 
            => Destroy(gameObject);
    }
}
