using Code.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Units.UI.FloatingDamages
{
    [RequireComponent(typeof(LookAtCanvasToCamera))]
    public class DamageFloatingView : MonoBehaviour, IView
    {
        [SerializeField] private float _floatDistance = 1.2f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _radius = 0.8f;

        private TextMeshPro _textMesh;
        private Tween _currentTween;
        private Color _originalColor;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
            _textMesh.sortingOrder = 1;
            _originalColor = _textMesh.color;
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
        }

        public void Show(float damage)
        {
            _currentTween?.Kill();

            Vector2 randomCircle = Random.insideUnitCircle * _radius;
            float directionX = randomCircle.x > 0 ? 0.5f : -0.5f;
            Vector3 offset = new Vector3(randomCircle.x, randomCircle.y + 1f, 0);

            transform.position = transform.parent.position + offset;
            transform.localScale = Vector3.one;

            _textMesh.text = Mathf.RoundToInt(damage).ToString();
            _textMesh.color = _originalColor;

            gameObject.SetActive(true);

            Vector3 targetPosition = transform.position + new Vector3(directionX, _floatDistance, 0);

            Sequence sequence = DOTween.Sequence();
            sequence.Join(transform.DOMove(targetPosition, _duration).SetEase(Ease.OutQuad));
            sequence.Join(_textMesh.DOColor(new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0), _duration)
                .SetEase(Ease.InQuad));
            sequence.OnComplete(() => gameObject.SetActive(false));

            _currentTween = sequence;
            sequence.Play();
        }

        public void Hide() 
            => gameObject.SetActive(false);
    }
}