using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.Services.Windows;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.Services.Windows;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IUIFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateUIRoot();
        GameObject CreateHud();
        UnitSpawnerViewsRoot CreateSpawnerViewsRoot();
        MeleePlayerUnitSpawnerPresenter CreateSpawnMeleePlayerUnitPresenter(Transform parent);
        RangedPlayerUnitSpawnerPresenter CreateSpawnRangedPlayerUnitPresenter(Transform parent);
        FightPresenter CreateFightPresenter();
        SettingsPresenter CreateSettingsPresenter();
        CurrencyPresenter CreateCurrencyPresenter();
        LevelPresenter CreateLevelPresenter();
        WindowBase CreateWindow(WindowType windowType);
        GameObject CreateWarningMessage(string message);
    }
}