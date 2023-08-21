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
        [SerializeField]
        protected float maxHealth;
        [SerializeField]
        protected float currentHealth;

        public float BaseHealth
        {
            get { return baseHealth; }
        }

        public float MaxHealth
        {
            get { return maxHealth; }
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
        }

        private void Start()
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
