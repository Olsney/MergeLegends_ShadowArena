using UnityEngine;

namespace Code.UI.Tutorial
{
    public class TutorialButtonHiderAndShower : MonoBehaviour
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}