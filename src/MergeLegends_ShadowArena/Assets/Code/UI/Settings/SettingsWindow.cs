using Code.UI.Services.Windows;

namespace Code.UI.Settings
{
    public class SettingsWindow : WindowBase
    {
        private void OnEnable()
        {
            transform.SetAsLastSibling();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            gameObject.SetActive(false);
        }
    }
}