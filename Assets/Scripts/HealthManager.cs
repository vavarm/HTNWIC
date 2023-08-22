using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace HTNWIC
{
    public abstract class HealthManager : NetworkBehaviour
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

        protected virtual void Start()
        {
            maxHealth = baseHealth;
            currentHealth = maxHealth;
        }

        private void Update()
        {
            if (isServer && Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(10f);
            }
        }

        public virtual void TakeDamage(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        protected abstract void Die();
    }
}
