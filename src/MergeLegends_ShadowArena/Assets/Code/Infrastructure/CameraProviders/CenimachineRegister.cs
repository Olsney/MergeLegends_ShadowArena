using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.CameraProviders
{
    public class CenimachineRegister : MonoBehaviour
    {
        private ICinemachineProvider _cinemachineProvider;

        [Inject]
        public void Construct(ICinemachineProvider cinemachineProvider)
            => _cinemachineProvider = cinemachineProvider;

        private void Awake()
        {
            _cinemachineProvider.SetImpulseSource(GetComponent<CinemachineImpulseSource>());
            _cinemachineProvider.SetVirtualCamera(GetComponent<CinemachineVirtualCameraBase>());
        }
    }
}