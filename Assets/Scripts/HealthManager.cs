using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace HTNWIC
{
    public abstract class HealthManager : NetworkBehaviour, IDamageable
    {
        [SerializeField]
        protected float baseHealth = 100f;
        [SyncVar]
        [SerializeField]
        protected float maxHealth;
        [SyncVar]
        [SerializeField]
        protected float currentHealth;

        public float BaseHealth => baseHealth;

        public float MaxHealth => maxHealth;

        public float CurrentHealth => currentHealth;

        public override void OnStartServer()
        {
            base.OnStartServer();
            maxHealth = baseHealth;
            currentHealth = maxHealth;
        }
        
        public abstract void TakeDamage(DamageData damageData);

        [Server]
        public virtual void Heal(float amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        protected abstract void Die();
    }
}
