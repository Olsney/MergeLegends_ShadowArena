using Code.Infrastructure.Factory.Units;
using Code.Services.StaticData;
using Code.Services.UnitStatsServices;
using Code.StaticData;
using Code.UI.Factory;
using Code.Units;
using Code.Units.Attack;
using Code.Units.Audio;
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
        private readonly IUnitStatsService _unitStatsService;

        private UnitTypeId _unitTypeId;
        private UnitAttackTypeId _unitAttackTypeId;
        private int _unitCurrentLevelIndex;
        private Transform _parent;
        private Vector3 _position;

        public UnitBuilder(
            IUnitFactory unitFactory,
            IStaticDataService staticData,
            IUnitPresenterFactory presenterFactory,
            IUnitStatsService unitStatsService)
        {
            _unitFactory = unitFactory;
            _staticData = staticData;
            _presenterFactory = presenterFactory;
            _unitStatsService = unitStatsService;
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

            UnitLevelStatsData stats = _unitStatsService.GetStats(unitStaticData, _unitCurrentLevelIndex);

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
                    var meleeAttackSound = unit.GetComponentInChildren<MeleeAttackSound>();

                    melee.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        meleeAttackSound: meleeAttackSound);

                    meleeAttackSound.Init(
                        audioClip: unitStaticData.GeneralData.UnitSoundData.MeleeAttackSoundData.hitAudio.AudioClip,
                        volume: unitStaticData.GeneralData.UnitSoundData.MeleeAttackSoundData.hitAudio.Volume);
                    break;
                case RangedAttacker ranged:
                    MagicSphereData magicSphereData = unitStaticData.GeneralData.MagicSphereData;
                    var rangedAttackSound = unit.GetComponentInChildren<RangedAttackSound>();

                    ranged.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        magicSphereSpeed: magicSphereData.Speed,
                        unitTypeId: _unitTypeId,
                        rangedAttackSound: rangedAttackSound);

                    rangedAttackSound.Init(
                        spawnClip: unitStaticData.GeneralData.UnitSoundData.RangedAttackSoundData.magicSphereAudio
                            .AudioClip,
                        spawnVolume: unitStaticData.GeneralData.UnitSoundData.RangedAttackSoundData.magicSphereAudio
                            .Volume,
                        explodeClip: unitStaticData.GeneralData.UnitSoundData.RangedAttackSoundData
                            .explosionMagicSphereAudio.AudioClip,
                        explodeVolume: unitStaticData.GeneralData.UnitSoundData.RangedAttackSoundData
                            .explosionMagicSphereAudio.Volume);
                    break;
                case BossAttacker boss:
                    var bossAttackSound = unit.GetComponentInChildren<BossAttackSound>();

                    bossAttackSound.Init(
                        attackClip: unitStaticData.GeneralData.UnitSoundData.BossAttackSoundData.clubSwingAudio
                            .AudioClip,
                        attackVolume: unitStaticData.GeneralData.UnitSoundData.BossAttackSoundData.clubSwingAudio
                            .Volume,
                        lastAttackClip: unitStaticData.GeneralData.UnitSoundData.BossAttackSoundData.groundSlamAudio
                            .AudioClip,
                        lastAttackVolume: unitStaticData.GeneralData.UnitSoundData.BossAttackSoundData.groundSlamAudio
                            .Volume);

                    boss.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        cameraShakeToAttackData: unitStaticData.GeneralData.CameraShakeToAttackData,
                        bossAttackSound: bossAttackSound);
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