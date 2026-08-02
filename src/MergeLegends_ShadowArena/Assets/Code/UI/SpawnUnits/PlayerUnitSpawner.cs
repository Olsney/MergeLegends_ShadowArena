using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Services.Audio;
using Code.StaticData;
using UnityEngine;

namespace Code.UI.SpawnUnits
{
    public class PlayerUnitSpawner : IPlayerUnitSpawner
    {
        private const int FirstLevelIndex = 0;

        private readonly IPlayerUnitCellsProvider _cellsProvider;
        private readonly IUnitBuilder _unitBuilder;
        private readonly IAudioService _audioService;

        public PlayerUnitSpawner(
            IPlayerUnitCellsProvider cellsProvider,
            IUnitBuilder unitBuilder,
            IAudioService audioService)
        {
            _cellsProvider = cellsProvider;
            _unitBuilder = unitBuilder;
            _audioService = audioService;
        }

        public void Spawn(UnitAttackTypeId attackTypeId)
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                Vector3 position = cell.Transform.position;

                cell.SetUnit(
                    _unitBuilder
                        .SetUnitType(UnitTypeId.PlayerUnit)
                        .SetUnitAttackType(attackTypeId)
                        .SetCurrentLevel(FirstLevelIndex)
                        .SetPosition(position)
                        .Build());
                
                _audioService.PlaySound(GameSoundTypeId.PlayerUnitSpawn);
                
                return;
            }
        }
    }
}