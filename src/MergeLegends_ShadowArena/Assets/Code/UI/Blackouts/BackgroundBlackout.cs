using UnityEngine;

namespace Code.UI.Blackouts
{
    public class BackgroundBlackout : MonoBehaviour
    {
        private void Awake() 
            => Hide();

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);
    }
}