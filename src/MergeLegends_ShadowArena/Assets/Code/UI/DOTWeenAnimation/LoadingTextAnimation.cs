using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class LoadingTextAnimation : MonoBehaviour
    {
        [SerializeField] private float _dotInterval;
        [SerializeField] private string _baseText = "Loading";

        private const int MaxDots = 3;
        private const char DotSymbol = '.';

        private TextMeshProUGUI _loadingText;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _loadingText = GetComponent<TextMeshProUGUI>();

            if (_loadingText == null)
                Debug.LogError("Missing TextMeshProUGUI");
        }

        private void OnEnable()
            => Play();

        private void OnDisable()
            => Stop();

        private void Play()
        {
            Stop();

            _cts = new CancellationTokenSource();
            StartAnimation(_cts.Token).Forget();
        }

        private void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask StartAnimation(CancellationToken token)
        {
            int dotCount = 0;

            while (token.IsCancellationRequested == false)
            {
                dotCount++;

                if (dotCount > MaxDots)
                    dotCount = 1;

                _loadingText.SetText(_baseText + new string(DotSymbol, dotCount));

                await UniTask.Delay(TimeSpan.FromSeconds(_dotInterval), cancellationToken: token);
            }
        }
    }
}