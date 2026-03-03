using UnityEngine;

namespace Code.Units.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public bool IsDead { get; private set; }
    
        private UnitDeath unitDeath;

        private void Awake()
        {
            unitDeath = GetComponent<UnitDeath>();
            GetComponent<IHealth>().InitConfig(100f);
        }

        private void Start()
        {
            unitDeath.Dead += OnDead;
        }

        private void OnDestroy()
        {
            unitDeath.Dead -= OnDead;
        }

        private void OnDead()
        {
            IsDead = true;
        }
    }
}