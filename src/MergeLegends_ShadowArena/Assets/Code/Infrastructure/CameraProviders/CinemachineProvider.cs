using Unity.Cinemachine;

namespace Code.Infrastructure.CameraProviders
{
    public class CinemachineProvider : ICinemachineProvider
    {
        public CinemachineVirtualCameraBase VirtualCamera { get; private set; }
        
        public CinemachineImpulseSource ImpulseSource { get; private set; }

        public void SetVirtualCamera(CinemachineVirtualCameraBase virtualCamera)
            => VirtualCamera = virtualCamera;

        public void SetImpulseSource(CinemachineImpulseSource impulseSource) 
            => ImpulseSource = impulseSource;
    }
}