using System.Collections.Generic;
using Code.ParticleBase;
using Code.Units.Attack;
using UnityEngine;

namespace Code.Units.Effects
{
    public class UnitEffects : MonoBehaviour
    {
        private readonly Dictionary<UnitEffectTypeId, BaseEffect> _effects = new();

        private void Awake()
        {
            Register<SpawnEffect>(UnitEffectTypeId.Spawn);
            Register<DamageEffect>(UnitEffectTypeId.Damage);
            Register<LevelUpEffect>(UnitEffectTypeId.LevelUp);
            Register<MergeHintEffect>(UnitEffectTypeId.Merge);
            Register<SelectedEffect>(UnitEffectTypeId.Selected);
            Register<BossLastAttackEffect>(UnitEffectTypeId.BossLastAttack);
        }

        public void Play(UnitEffectTypeId typeId)
        {
            if (_effects.TryGetValue(typeId, out BaseEffect effect))
                effect.Play();
        }

        public void Stop(UnitEffectTypeId typeId)
        {
            if (_effects.TryGetValue(typeId, out BaseEffect effect))
                effect.Stop();
        }

        private void Register<T>(UnitEffectTypeId typeId) where T : BaseEffect
        {
            T effect = GetComponentInChildren<T>(true);

            if (effect != null)
                _effects[typeId] = effect;
        }
    }
}