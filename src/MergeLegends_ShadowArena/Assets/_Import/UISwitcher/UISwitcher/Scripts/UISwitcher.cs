using System;
using UISwitcher;
using UnityEngine;
using UnityEngine.UI;

namespace _Import.UISwitcher.UISwitcher.Scripts
{
    public class UISwitcher : UINullableToggle
    {
        private readonly Vector2 _min = new(0, 0.5f);
        private readonly Vector2 _max = new(1, 0.5f);
        private readonly Vector2 _middle = new(0.5f, 0.5f);

        [SerializeField] private Graphic backgroundGraphic;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        [SerializeField] private Color nullColor;
        [SerializeField] private RectTransform tipRect;

        public event Action<bool?> Changed;

        private Color BackgroundColor
        {
            set
            {
                if (backgroundGraphic == null)
                    return;

                backgroundGraphic.color = value;
            }
        }
        
        private bool _ignoreChange;

        public void SetValue(bool? value)
        {
            _ignoreChange = true;

            isOnNullable = value;

            UpdateView(value);

            _ignoreChange = false;
        }

        protected override void OnChanged(bool? value)
        {
            UpdateView(value);

            if (_ignoreChange)
                return;

            Changed?.Invoke(value);
        }

        private void UpdateView(bool? value)
        {
            switch (value)
            {
                case null:
                    SetNull();
                    break;

                case true:
                    SetOn();
                    break;

                case false:
                    SetOff();
                    break;
            }
        }


        private void SetOn()
        {
            SetAnchors(_max);
            BackgroundColor = onColor;
        }


        private void SetOff()
        {
            SetAnchors(_min);
            BackgroundColor = offColor;
        }


        private void SetNull()
        {
            SetAnchors(_middle);
            BackgroundColor = nullColor;
        }


        private void SetAnchors(Vector2 anchor)
        {
            tipRect.anchorMin = anchor;
            tipRect.anchorMax = anchor;
            tipRect.pivot = anchor;
        }
    }
}