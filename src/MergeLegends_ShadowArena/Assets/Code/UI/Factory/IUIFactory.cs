using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.Services.Windows;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.Services.Windows;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using Code.UI.SpawnUnits.PriceUnits;
using Code.UI.WarningMessages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
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
        MeleePlayerUnitPurchaseView CreateMeleeSpawnButtonView(Transform parent);
        RangedPlayerUnitPurchaseView CreateRangedSpawnButtonView(Transform parent);
        StartFightView CreateStartFightButtonView();
        WarningMessageView CreateWarningMessageView();
        OpenSettingsView GetOpenSettingsView();
        CurrencyCounterView GetCurrencyCounterView();
        LevelView GetLevelView();
        public UnitHealthBarView GetUnitHealthBarView();
        public UnitLevelView GetUnitLevelView();
        WindowBase CreateWindow(WindowType windowType);
    }
}