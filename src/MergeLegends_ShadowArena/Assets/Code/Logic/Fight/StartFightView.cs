using System;
using Code.Logic.ViewProvider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.Fight
{
    public class StartFightView : MonoBehaviour, IStartFightView
    {
        [SerializeField] private Button _fightButton;

        private IViewProvider _viewProvider;
        
        public event Action FightClicked;

        private void Awake() 
            => _viewProvider.AddView(this);

        [Inject]
        public void Construct(IViewProvider viewProvider) 
            => _viewProvider = viewProvider;

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);

        private void OnEnable() => _fightButton.onClick.AddListener(OnFightButtonClick);

        private void OnDisable() => _fightButton.onClick.RemoveListener(OnFightButtonClick);

        private void OnFightButtonClick()
            => FightClicked?.Invoke();
    }
}