using Code.UI.ReloadScenes;
using Code.UI.Rewards;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.UI.VictoryShowers
{
    public class VictoryView : MonoBehaviour, IView
    {
        private RewardView _rewardView;
        private ReloadSceneView _reloadSceneView;
        private ContentVictoryWindowAnimation _contentVictoryWindowAnimation;

        private void Awake()
        {
            transform.SetAsLastSibling();
            _rewardView = GetComponentInChildren<RewardView>();
            _contentVictoryWindowAnimation = GetComponentInChildren<ContentVictoryWindowAnimation>();
            _reloadSceneView = GetComponentInChildren<ReloadSceneView>();
        }

        public void SetValueReward(long rewardValue)
        {
            _rewardView.SetValue(rewardValue);
        }

        public async UniTask PlayContentAnimationAsync()
        {
            await _contentVictoryWindowAnimation.StartAnimationAsync();
        }

        public async UniTask PlayRewardAnimationAsync()
        {
            await _rewardView.StartAnimationAsync();
        }

        public void ShowNextLevelButton()
        {
            _reloadSceneView.StartAnimation();
        }
    }
}