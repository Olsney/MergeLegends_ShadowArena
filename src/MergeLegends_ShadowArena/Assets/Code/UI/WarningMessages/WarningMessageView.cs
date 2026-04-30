using UnityEngine;

namespace Code.UI.WarningMessages
{
    public class WarningMessageView : MonoBehaviour
    {
        private WarningMessageText _messageText;

        private void Awake()
            => _messageText = GetComponentInChildren<WarningMessageText>();

        public void SetValue(string message)
        {
            if (_messageText == null)
            {
                Debug.LogError("MessageText is null — cannot set message");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                Debug.LogWarning("Empty message — nothing to display");
                return;
            }

            _messageText.TextMeshPro.text = message.ToUpper();
        }
    }
}