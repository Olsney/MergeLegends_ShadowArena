using Code.UI.SpawnUnits;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IPurchasePresenterFactory
    {
        public MeleePlayerUnitPurchasePresenter CreateMeleePlayerUnitPurchasePresenter(Transform parent);
        public RangedPlayerUnitPurchasePresenter CreateRangedPlayerUnitPurchasePresenter(Transform parent);
    }
}