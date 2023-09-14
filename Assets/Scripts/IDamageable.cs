using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTNWIC
{
    public interface IDamageable
    {
        public void TakeDamage(float amount);
        public void TakeDamage(DamageData damageData);
    }
}
