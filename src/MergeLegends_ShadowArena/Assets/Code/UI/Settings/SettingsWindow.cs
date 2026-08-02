using Code.UI.Services.Windows;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.UI.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        
        private void OnEnable()
            => transform.SetAsLastSibling();
    }
}