using System;
using UnityEngine;

namespace Code.UI.Settings.MusicMute
{
    public class MusicMuteView : MonoBehaviour, IView
    {
        private _Import.UISwitcher.UISwitcher.Scripts.UISwitcher _switcher;

        public event Action<bool?> Changed;

        private void Awake()
        {
            _switcher = GetComponent<_Import.UISwitcher.UISwitcher.Scripts.UISwitcher>();
        }

        private void Start()
            => _switcher.Changed += OnSwitcherChanged;

        private void OnDestroy()
            => _switcher.Changed -= OnSwitcherChanged;

        public void SetValue(bool value) 
            => _switcher.SetValue(value);

        private void OnSwitcherChanged(bool? value)
            => Changed?.Invoke(value);
    }
}