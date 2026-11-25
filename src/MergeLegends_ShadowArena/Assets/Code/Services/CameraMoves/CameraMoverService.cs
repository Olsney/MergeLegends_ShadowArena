using Code.Infrastructure.CameraProviders;
using Code.Services.StaticData;
using Code.StaticData;
using DG.Tweening;
using Unity.Cinemachine;

namespace Code.Services.CameraMoves
{
    public class CameraMoverService : ICameraMoverService
    {
        private readonly ICinemachineProvider _cinemachineProvider;
        private readonly IStaticDataService _staticDataService;

        public CameraMoverService(ICinemachineProvider cinemachineProvider, IStaticDataService staticDataService)
        {
            _cinemachineProvider = cinemachineProvider;
            _staticDataService = staticDataService;
        }

        public void StartMoveFirstTimeGameLater()
            => MoveCamera(CameraMoveTypeId.StartGame);

        public void StartMoveToFight()
            => MoveCamera(CameraMoveTypeId.StartFight);

        private void MoveCamera(CameraMoveTypeId typeId)
        {
            var settings = _staticDataService.GetCameraMoveData(typeId).GeneralSettings;
            
            _cinemachineProvider.VirtualCamera.transform.DOMove(settings.TargetPosition, settings.Duration)
                .SetEase(Ease.InOutQuad);
        }
    }
}