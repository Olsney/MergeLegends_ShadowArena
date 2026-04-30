using DG.Tweening;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class RiseAndVanishAnimation : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 100f;
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private float lifeTime = 2f;

        private void Start()
        {
            transform.DOMoveY(transform.position.y + moveDistance, duration);
            GetComponent<CanvasGroup>().DOFade(0, duration);
            Destroy(gameObject, lifeTime);
        }
    }
}
