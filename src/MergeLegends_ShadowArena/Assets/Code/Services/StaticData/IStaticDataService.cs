using System.Collections.Generic;
using Code.Services.Windows;
using Code.StaticData;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        GameStaticData GetGameStaticData(string sceneKey);
        UnitStaticData GetPlayerUnit(UnitAttackTypeId unitAttackTypeId);
        UnitStaticData GetEnemy(UnitAttackTypeId unitAttackTypeId);
        GridPlacesStaticData GetGridPlaces(UnitTypeId unitTypeId);
        WindowData GetWindow(WindowType windowType);
        WarningMessageData GetWarningMessage(WarningMessagesTypeId warningTypeId);
        CameraMoveData GetCameraMoveData(CameraMoveTypeId cameraMoveTypeId);
        AudioData GetSoundData(GameSoundTypeId gameSoundTypeId);
        List<GameMusicData> GetMusicData();
        TutorialStaticData GetTutorialData();
    }
}