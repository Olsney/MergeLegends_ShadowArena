using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.Services.Windows;
using Code.UI.Fights;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IUIFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateUIRoot();
        GameObject CreateHud();
        UnitSpawnerViewsRoot CreateSpawnerViewsRoot();
        MeleePlayerUnitSpawnerPresenter CreateSpawnMeleePlayerUnitPresenter(Transform parent);
        RangedPlayerUnitSpawnerPresenter CreateSpawnRangedPlayerUnitPresenter(Transform parent);
        FightPresenter CreateFightPresenter();
        SettingsPresenter CreateSettingsPresenter();
    }
}