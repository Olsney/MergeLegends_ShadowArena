using System;
using Code.Services.PersistentProgress;
using Code.Services.PlayerUnitPurchases;
using Code.UI.SpawnUnits;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class PurchaseUnitPresenterFactory : IPurchasePresenterFactory
    {
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IPlayerUnitPurchaseService _purchaseService;

        public PurchaseUnitPresenterFactory(
            IUIFactory uiFactory,
            IPersistentProgressService progressService,
            IPlayerUnitPurchaseService purchaseService)
        {
            _uiFactory = uiFactory;
            _progressService = progressService;
            _purchaseService = purchaseService;
        }

        public MeleePlayerUnitPurchasePresenter CreateMeleePlayerUnitPurchasePresenter(Transform parent)
        {
            var view = _uiFactory.CreateMeleeSpawnButtonView(parent);
            return CreatePresenter(view,
                purchaseView => new MeleePlayerUnitPurchasePresenter(purchaseView, _purchaseService, _progressService));
        }

        public RangedPlayerUnitPurchasePresenter CreateRangedPlayerUnitPurchasePresenter(Transform parent)
        {
            var view = _uiFactory.CreateRangedSpawnButtonView(parent);
            return CreatePresenter(view,
                purchaseView => new RangedPlayerUnitPurchasePresenter(purchaseView, _purchaseService, _progressService));
        }

        private TPresenter CreatePresenter<TView, TPresenter>(TView view, Func<TView, TPresenter> factory)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            TPresenter presenter = factory(view);
            presenter.Initialize();
            return presenter;
        }
    }
}