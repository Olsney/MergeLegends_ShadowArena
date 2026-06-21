using System;
using Code.UI.DOTWeenAnimation;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ReloadScenes
{
    public class ReloadSceneView : MonoBehaviour, IView
    {
        [SerializeField] private Button _restartLevelButton;

        public event Action RestartLevelClicked;

        private ButtonAppearAnimation _buttonAppearAnimation;
        private ButtonIdleAnimation _buttonIdleAnimation;

        private void Awake()
        {
            _buttonAppearAnimation = GetComponent<ButtonAppearAnimation>();
            _buttonIdleAnimation = GetComponent<ButtonIdleAnimation>();
        }

        public void StartAnimation()
        {
            _buttonIdleAnimation.StartAnimation();
            _buttonAppearAnimation.StartAnimation();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
            => _restartLevelButton.onClick.AddListener(OnRestartLevelButtonClicked);

        private void OnDisable()
            => _restartLevelButton.onClick.RemoveListener(OnRestartLevelButtonClicked);

        private void OnRestartLevelButtonClicked()
            => RestartLevelClicked?.Invoke();
    }
}