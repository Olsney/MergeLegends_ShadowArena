using Code.UI.Blackouts;
using Code.UI.Services.Windows;

namespace Code.UI.Settings
{
    public class SettingsWindow : WindowBase
    {
        private BackgroundBlackout _backgroundBlackout;

        private void OnEnable()
        {
            transform.SetAsLastSibling();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _backgroundBlackout = GetComponentInChildren<BackgroundBlackout>();
            gameObject.SetActive(false);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _backgroundBlackout.Show();
        }

        protected override void OnClose()
        {
            base.OnClose();
            _backgroundBlackout.Hide();
        }
    }
}