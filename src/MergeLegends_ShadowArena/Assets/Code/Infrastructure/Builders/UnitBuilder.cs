using System;
using Code.Infrastructure.Factory.Units;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factory;
using Code.Units;
using Code.Units.Attack;
using Code.Units.PlayerUnits;
using Code.Units.UI;
using Code.Units.UI.FloatingDamages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
using UnityEngine;

namespace Code.Infrastructure.Builders
{
    public class UnitBuilder : IUnitBuilder
    {
        private readonly IUnitFactory _unitFactory;
        private readonly IStaticDataService _staticData;
        private readonly IUnitPresenterFactory _presenterFactory;

        private UnitTypeId _unitTypeId;
        private UnitAttackTypeId _unitAttackTypeId;
        private int _unitCurrentLevelIndex;
        private Transform _parent;
        private Vector3 _position;

        public UnitBuilder(
            IUnitFactory unitFactory,
            IStaticDataService staticData,
            IUnitPresenterFactory presenterFactory)
        {
            _unitFactory = unitFactory;
            _staticData = staticData;
            _presenterFactory = presenterFactory;
        }

        public IUnitBuilder SetUnitType(UnitTypeId unitTypeId)
        {
            _unitTypeId = unitTypeId;
            return this;
        }

        public IUnitBuilder SetUnitAttackType(UnitAttackTypeId unitAttackTypeId)
        {
            _unitAttackTypeId = unitAttackTypeId;
            return this;
        }

        public IUnitBuilder SetCurrentLevel(int levelIndex)
        {
            _unitCurrentLevelIndex = levelIndex;
            return this;
        }

        public IUnitBuilder SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public GameObject Build()
        {
            GameObject unit = _unitFactory.CreateUnit(
                _unitTypeId, _unitAttackTypeId, _unitCurrentLevelIndex, _position);

            InitGameplay(unit);
            InitPresenters(unit);

            return unit;
        }

        private void InitGameplay(GameObject unit)
        {
            UnitStaticData unitStaticData = GetUnitStaticData();

            if (unitStaticData.UnitStatsByLevelIndexSetups == null ||
                unitStaticData.UnitStatsByLevelIndexSetups.Count == 0)
                throw new InvalidOperationException($"UnitStatsByLevelIndexSetups is empty for {unitStaticData.name}");

            if (_unitCurrentLevelIndex < 0 ||
                _unitCurrentLevelIndex >= unitStaticData.UnitStatsByLevelIndexSetups.Count)
                throw new ArgumentOutOfRangeException(nameof(_unitCurrentLevelIndex),
                    $"Level {_unitCurrentLevelIndex} not found in {unitStaticData.name}");

            UnitLevelStatsData stats = unitStaticData.UnitStatsByLevelIndexSetups[_unitCurrentLevelIndex]
                .UnitLevelStatsData;
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;

            if (unit.TryGetComponent<PlayerUnit>(out var playerUnit))
                playerUnit.Init(_unitCurrentLevelIndex, _unitAttackTypeId);

            IHealth health = unit.GetComponent<IHealth>();
            health.Init(stats.Hp);
            unit.GetComponent<ITargetFinder>().Init(targetData.TargetLayerMask);
            unit.GetComponent<UnitNavmeshAgentMover>().InitConfig(unitData.MoveSpeed, unitData.AttackDistance);

            switch (unit.GetComponentInChildren<IAttacker>())
            {
                case MeleeAttacker melee:
                    melee.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask);
                    break;
                case RangedAttacker ranged:
                    MagicSphereData magicSphereData = unitStaticData.GeneralData.MagicSphereData;

                    ranged.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        magicSphereSpeed: magicSphereData.Speed,
                        unitTypeId: _unitTypeId);
                    break;
                case BossAttacker boss:
                    boss.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        cameraShakeToAttackData: unitStaticData.GeneralData.CameraShakeToAttackData);
                    break;
            }
        }

        private void InitPresenters(GameObject unitPrefab)
        {
            IUnit unit = unitPrefab.GetComponent<IUnit>();
            IHealth health = unitPrefab.GetComponent<IHealth>();
            UnitDeath unitDeath = unitPrefab.GetComponent<UnitDeath>();

            UnitCanvasView canvasView = unitPrefab.GetComponentInChildren<UnitCanvasView>();
            UnitLevelView levelView = unitPrefab.GetComponentInChildren<UnitLevelView>();
            UnitHealthBarView healthBarView = unitPrefab.GetComponentInChildren<UnitHealthBarView>();
            DamageFloatingView damageFloatingView = unitPrefab.GetComponentInChildren<DamageFloatingView>();

            UnitCanvasPresenter canvasPresenter =
                _presenterFactory.CreateUnitCanvasPresenter(canvasView, unitDeath);
            UnitLevelPresenter levelPresenter =
                _presenterFactory.CreateUnitLevelPresenter(levelView, _unitCurrentLevelIndex);
            UnitHealthBarPresenter healthBarPresenter =
                _presenterFactory.CreateUnitHealthBarPresenter(healthBarView, health);
            DamageFloatingPresenter damageFloatingPresenter =
                _presenterFactory.CreateDamageFloatingPresenter(damageFloatingView, health);

            unit.Destroying += OnUnitDestroying;

            void OnUnitDestroying()
            {
                unit.Destroying -= OnUnitDestroying;

                levelPresenter?.Dispose();
                healthBarPresenter?.Dispose();
                damageFloatingPresenter?.Dispose();
                canvasPresenter?.Dispose();

                levelPresenter = null;
                healthBarPresenter = null;
                damageFloatingPresenter = null;
                canvasPresenter = null;
            }
        }

        private UnitStaticData GetUnitStaticData()
        {
            switch (_unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    return _staticData.GetPlayerUnit(_unitAttackTypeId);
                case UnitTypeId.Enemy:
                    return _staticData.GetEnemy(_unitAttackTypeId);
                default:
                    return null;
            }
        }
    }
}