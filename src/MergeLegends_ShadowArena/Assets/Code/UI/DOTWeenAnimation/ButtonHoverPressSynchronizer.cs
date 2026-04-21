using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class ButtonHoverPressSynchronizer : MonoBehaviour
    {
        [SerializeField] private ButtonHoverAnimation _hoverAnimation;
        [SerializeField] private ButtonPressAnimation _pressAnimation;

        private bool _isPressed;

        private void Awake()
        {
            _hoverAnimation = GetComponent<ButtonHoverAnimation>();
            _pressAnimation = GetComponent<ButtonPressAnimation>();
        }

        private void OnEnable()
        {
            _pressAnimation.OnPressed += HandlePressed;
            _pressAnimation.OnReleased += HandleReleased;
            _hoverAnimation.OnHoverStateChanged += HandleHoverStateChanged;
        }

        private void OnDisable()
        {
            _pressAnimation.OnPressed -= HandlePressed;
            _pressAnimation.OnReleased -= HandleReleased;
            _hoverAnimation.OnHoverStateChanged -= HandleHoverStateChanged;
        }

        private void HandlePressed() 
            => _isPressed = true;

        private void HandleReleased()
        {
            _isPressed = false;

            if (_hoverAnimation.IsHovering)
            {
                _hoverAnimation.ApplyHoverScale();
            }
        }

        private void HandleHoverStateChanged(bool isHovering)
        {
            if (_isPressed) 
                return;

            if (isHovering)
            {
                _hoverAnimation.ApplyHoverScale();
            }
            else
            {
                _hoverAnimation.ReturnToOriginalScale();
            }
        }
    }
}