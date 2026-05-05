using Code.UI;
using UnityEngine;

namespace Code.Units.UI
{
    public class UnitCanvasView : MonoBehaviour, IView
    {
        public void Remove() 
            => Destroy(gameObject);
    }
}