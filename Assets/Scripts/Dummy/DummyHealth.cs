using UnityEngine;
using FishNet.Object;

namespace HTNWIC.Dummy
{
    [RequireComponent(typeof(DummyHealthBar))]
    public class DummyHealth : HealthManager
    {
        [SerializeField]
        private DummyHealthBar healthBar;

        public override void OnStartServer()
        {
            base.OnStartServer();
            healthBar = GetComponent<DummyHealthBar>();
            healthBar.SetHealthBarValue(CurrentHealth, MaxHealth);
        }

        private void Update()
        {
            if (base.IsServerOnly && Input.GetKeyDown(KeyCode.K))
            {
                /* Old way to deal damage
                TakeDamage(10f);
                */
                // New way to deal damage
                // Parameters:
                // 1. The source of the damage (GameObject)
                // 2. The amount of physical damage (float)
                // 3. The amount of magical damage (float)
                // 4. The amount of true damage (float)
                // 5. The amount of physical penetration (float)
                // 6. The amount of magical penetration (float)
                // 7. The amount of lifesteal (float) (to heal the source of the damage)
                DamageData damageData = new DamageData(null, 10f, 5f, 0f, 0f, 0f, 0f);
                TakeDamage(damageData);
            }
        }

        protected override void Die()
        {
            Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has died");
            Destroy(gameObject);
        }

        [Server]
        public override void TakeDamage(DamageData damageData)
        {
            // calculate total damage
            float totalDamage = damageData.PhysicalDamageAmount + damageData.MagicalDamageAmount + damageData.TrueDamageAmount;
            // apply damage
            currentHealth -= totalDamage;
            // check if dead
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            // update health bar
            healthBar.SetHealthBarValue(CurrentHealth, MaxHealth);
            // calculate lifesteal
            float lifesteal = totalDamage * damageData.LifeStealPercentage;
            // heal the attacker for the lifesteal amount
            if (lifesteal > 0f)
            {
                if (damageData.Attacker != null && damageData.Attacker.GetComponent<HealthManager>() != null)
                {
                    damageData.Attacker.GetComponent<HealthManager>().Heal(lifesteal);
                }
                else
                {
                    Debug.LogWarning($"Attacker of dummy no.{gameObject.GetInstanceID()} is null or does not have a HealthManager component");
                }
            }
            // log damage
            if (CurrentHealth > 0) Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has taken {totalDamage} damage. He has {CurrentHealth} health left");
        }

        [Server]
        public override void Heal(float amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            healthBar.SetHealthBarValue(CurrentHealth, MaxHealth);
            Debug.Log($"Dummy no.{gameObject.GetInstanceID()} has been healed for {amount} health. He has {CurrentHealth} health left");
        }
    }
}
