using Code.UI.ReloadScenes;
using Code.UI.Rewards;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.UI.LossShowers
{
    public class LoseView : MonoBehaviour, IView
    {
        private RewardView _rewardView;
        private ReloadSceneView _reloadSceneView;
        private ContentLoseWindowAnimation _contentVictoryWindowAnimation;

        private void Awake()
        {
            transform.SetAsLastSibling();
            _rewardView = GetComponentInChildren<RewardView>();
            _contentVictoryWindowAnimation = GetComponentInChildren<ContentLoseWindowAnimation>();
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

        public void ShowReloadButton()
        {
            _reloadSceneView.StartAnimation();
        }
    }
}