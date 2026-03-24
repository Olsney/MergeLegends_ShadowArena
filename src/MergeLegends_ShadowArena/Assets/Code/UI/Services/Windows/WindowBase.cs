using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Services.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        //TODO: Дописать сюда когда будет что. 
        // protected IWorldData WorldData;

        [SerializeField] private Button _closeButton;
        // protected int Score => WorldData.Score;

        // [Inject]
        // public void Construct(IWorldData worldData)
        // {
        //   WorldData = worldData;
        // }

        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        public void Open()
        {
            OnOpen();
        }

        protected virtual void OnAwake()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Close);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }

        protected virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnClose()
        {
            gameObject.SetActive(false);
        }

        private void Close()
        {
            OnClose();
        }
    }
}