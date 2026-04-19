using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Fights
{
    public class StartFightView : MonoBehaviour, IView
    {
        [SerializeField] private Button _fightButton;
        
        public event Action FightClicked;

        public void Remove() 
            => Destroy(gameObject);

        private void OnEnable() 
            => _fightButton.onClick.AddListener(OnFightButtonClick);

        private void OnDisable() 
            => _fightButton.onClick.RemoveListener(OnFightButtonClick);

        private void OnFightButtonClick()
            => FightClicked?.Invoke();
    }
}