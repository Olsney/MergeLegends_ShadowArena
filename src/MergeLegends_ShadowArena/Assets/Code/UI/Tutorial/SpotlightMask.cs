using Code.Infrastructure.CameraProviders;
using Code.Services.StaticData;
using Code.Services.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Tutorial
{
    [RequireComponent(typeof(Image))]
    public class SpotlightMask : MonoBehaviour, ICanvasRaycastFilter
    {
        private static readonly int ColorId = Shader.PropertyToID("_Color");
        private static readonly int CenterId = Shader.PropertyToID("_Center");
        private static readonly int RadiusId = Shader.PropertyToID("_Radius");
        private static readonly int AspectId = Shader.PropertyToID("_Aspect");

        private ICameraProvider _cameraProvider;
        private ITutorialPointerService _pointerService;
        private IStaticDataService _staticDataService;

        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Material _material;
        private RectTransform _uiTarget;
        private Transform _worldTarget;
        private Camera _canvasCamera;
        private Vector2 _spotlightPosition;
        private float _radius;
        private float _raycastRadiusMultiplier;
        private float _raycastSqrRadius;
        private bool _pointerActive;
        private bool _refreshRequested;
        private TutorialPointerSide _pointerSide;

        [Inject]
        private void Construct(ICameraProvider cameraProvider, ITutorialPointerService pointerService,
            IStaticDataService staticDataService)
        {
            _cameraProvider = cameraProvider;
            _pointerService = pointerService;
            _staticDataService = staticDataService;
        }

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _canvas = GetComponentInParent<Canvas>();

            TutorialStaticData config = _staticDataService.GetTutorialData();

            _raycastRadiusMultiplier = config.RaycastRadiusMultiplier;

            Image image = GetComponent<Image>();
            _material = new Material(image.material);
            image.material = _material;
        }

        private void OnRectTransformDimensionsChange() =>
            _refreshRequested = _uiTarget != null || _worldTarget != null;

        private void LateUpdate()
        {
            if (_refreshRequested == false)
                return;

            Canvas.ForceUpdateCanvases();
            Refresh();
        }

        private void OnDestroy()
        {
            _pointerService.Hide();
            Destroy(_material);
        }

        public void ShowUI(RectTransform target, float radius = 100f, bool isPointerActive = true,
            TutorialPointerSide pointerSide = TutorialPointerSide.Left)
        {
            Prepare(radius, isPointerActive, pointerSide);
            Canvas.ForceUpdateCanvases();

            _uiTarget = target;
            _worldTarget = null;

            Refresh();
        }

        public void ShowWorld(Transform target, float radius = 100f, bool isPointerActive = true,
            TutorialPointerSide pointerSide = TutorialPointerSide.Left)
        {
            Prepare(radius, isPointerActive, pointerSide);

            _worldTarget = target;
            _uiTarget = null;

            Refresh();
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide()
        {
            _pointerService.Hide();
            _uiTarget = null;
            _worldTarget = null;
            _pointerActive = false;
            _refreshRequested = false;
            gameObject.SetActive(false);
        }

        public void Refresh()
        {
            _refreshRequested = false;

            if (TryGetTargetScreenPoint(out Vector2 screenPoint) == false)
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                screenPoint,
                _canvasCamera,
                out _spotlightPosition);

            ApplyShader();
            UpdatePointer();
        }

        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                screenPoint,
                _canvasCamera,
                out Vector2 localPoint);

            return (localPoint - _spotlightPosition).sqrMagnitude > _raycastSqrRadius;
        }

        private void Prepare(float radius, bool pointerActive, TutorialPointerSide pointerSide)
        {
            ApplyRadius(radius);

            _pointerActive = pointerActive;
            _pointerSide = pointerSide;
            _canvasCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _canvas.worldCamera;

            Show();
        }

        private bool TryGetTargetScreenPoint(out Vector2 screenPoint)
        {
            if (_uiTarget != null)
            {
                screenPoint = SpotlightTargetProjector.ProjectUI(_uiTarget, _canvasCamera);
                return true;
            }

            if (_worldTarget != null)
                return SpotlightTargetProjector.TryProjectWorld(_worldTarget, _cameraProvider.MainCamera,
                    out screenPoint);

            screenPoint = default;
            return false;
        }

        private void ApplyRadius(float radius)
        {
            _radius = Mathf.Max(0f, radius);

            float raycastRadius = _radius * _raycastRadiusMultiplier;
            _raycastSqrRadius = raycastRadius * raycastRadius;
        }

        private void ApplyShader()
        {
            Vector2 size = _rectTransform.rect.size;

            Vector2 center = new(_spotlightPosition.x / size.x + 0.5f, _spotlightPosition.y / size.y + 0.5f);

            _material.SetVector(CenterId, center);
            _material.SetFloat(RadiusId, _radius / size.y);
            _material.SetFloat(AspectId, size.x / size.y);
        }

        private void UpdatePointer()
        {
            if (_pointerActive == false)
            {
                _pointerService.Hide();
                return;
            }

            _pointerService.Show(_rectTransform, _spotlightPosition, _radius, _pointerSide);
        }
    }
}