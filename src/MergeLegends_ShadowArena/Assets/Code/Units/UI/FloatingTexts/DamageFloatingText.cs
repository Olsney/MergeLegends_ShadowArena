using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Units.UI.FloatingTexts
{
    [RequireComponent(typeof(LookAtCanvasToCamera))]
    public class DamageFloatingText : MonoBehaviour
    {
        [SerializeField] private float _floatDistance = 1.2f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _radius = 0.8f;

        private IHealth _health;
        private float _lastHealthValue;
        private TextMeshPro _textMesh;
        private Tween _currentTween;
        private Color _originalColor;

        private void Awake()
        {
            _health = GetComponentInParent<IHealth>();
            _textMesh = GetComponent<TextMeshPro>();
            _textMesh.sortingOrder = 1;
            _originalColor = _textMesh.color;
        }

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
            _lastHealthValue = _health.CurrentValue;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
            _currentTween?.Kill();
        }

        private void OnHealthChanged()
        {
            float damage = _lastHealthValue - _health.CurrentValue;
            _lastHealthValue = _health.CurrentValue;

            if (damage > 0)
            {
                ShowDamage((int)damage);
            }
        }

        private void ShowDamage(int damage)
        {
            _currentTween?.Kill();

            Vector2 randomCircle = Random.insideUnitCircle * _radius;
            float directionX = randomCircle.x > 0 ? 0.5f : -0.5f;
            Vector3 offset = new Vector3(randomCircle.x, randomCircle.y + 1f, 0);

            transform.position = transform.parent.position + offset;
            transform.localScale = Vector3.one;

            _textMesh.text = damage.ToString();
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
    }
}