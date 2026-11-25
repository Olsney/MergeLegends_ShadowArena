using Unity.Cinemachine;

namespace Code.Infrastructure.CameraProviders
{
    public interface ICinemachineProvider
    {
        CinemachineVirtualCameraBase VirtualCamera { get; }
        CinemachineImpulseSource ImpulseSource { get; }
        void SetVirtualCamera(CinemachineVirtualCameraBase virtualCamera);
        void SetImpulseSource(CinemachineImpulseSource impulseSource);
    }
}