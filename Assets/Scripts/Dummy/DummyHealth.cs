using System.Collections;
using System.Collections.Generic;
using HTNWIC;
using UnityEngine;
using Mirror;

namespace HTNWIC.Dummy
{
    [RequireComponent(typeof(DummyHealthBar))]
    public class DummyHealth : HealthManager
    {
        [SerializeField]
        private DummyHealthBar healthBar;

        protected override void Start()
        {
            base.Start();
            healthBar = GetComponent<DummyHealthBar>();
            healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
        }

        protected override void Die()
        {
            Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has died");
            Destroy(gameObject);
        }

        [Server]
        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            if (!isServer) return;
            healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
            if (CurrentHealth > 0) Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has taken {amount} damage. He has {CurrentHealth} health left");
        }
    }
}
