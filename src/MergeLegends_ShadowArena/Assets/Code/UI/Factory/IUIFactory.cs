using UnityEngine;

namespace Code.UI.Factory
{
    public interface IUIFactory
    {
        GameObject CreateUIRoot();
        GameObject CreateHud();
        PlayerSpawnerPresenter CreateSpawnPlayerUnitPresenter();
        FightPresenter CreateFightPresenter();
    }
}