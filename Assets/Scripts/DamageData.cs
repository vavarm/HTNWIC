using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace HTNWIC
{
    [System.Serializable]
    public class DamageData
    {
        public struct DamageOverTimeData
        {
            public float damageAmount;
            public float duration;
            public float tickRate;
        }

        [Header("Damage Amount")]
        private float physicalDamageAmount = 0f;
        public float PhysicalDamageAmount => physicalDamageAmount;
        private float magicDamageAmount = 0f;
        public float MagicDamageAmount => magicDamageAmount;
        private float trueDamageAmount = 0f;
        public float TrueDamageAmount => trueDamageAmount;
        [Header("Additional Damage Data")]
        private float physicalDamagePenetration = 0f;
        public float PhysicalDamagePenetration => physicalDamagePenetration;
        private float magicPenetration = 0f;
        public float MagicPenetration => magicPenetration;
        [Header("Lifesteal")]
        private float lifeStealPercentage = 0f;
        public float LifeStealPercentage => lifeStealPercentage;
        /*
        [Header("Damage Over Time")]
        private DamageOverTimeData physicalDamageOverTime;
        public DamageOverTimeData PhysicalDamageOverTime => physicalDamageOverTime;
        private DamageOverTimeData magicDamageOverTime;
        public DamageOverTimeData MagicDamageOverTime => magicDamageOverTime;
        private DamageOverTimeData trueDamageOverTime;
        public DamageOverTimeData TrueDamageOverTime => trueDamageOverTime;
        */

        public DamageData(float _physicalDamageAmount, float _magicDamageAmount, float _trueDamageAmount, float _physicalDamagePenetration, float _magicPenetration, float _lifeStealPercentage, DamageOverTimeData? _physicalDamageOverTime = null, DamageOverTimeData? _magicDamageOverTime = null, DamageOverTimeData? _trueDamageOverTime = null)
        {
            if (physicalDamageAmount < 0f)
            {
                physicalDamageAmount = 0f;
            }
            this.physicalDamageAmount = _physicalDamageAmount;
            if (magicDamageAmount < 0f)
            {
                magicDamageAmount = 0f;
            }
            this.magicDamageAmount = _magicDamageAmount;
            if (trueDamageAmount < 0f)
            {
                trueDamageAmount = 0f;
            }
            this.trueDamageAmount = _trueDamageAmount;
            if (physicalDamagePenetration < 0f)
            {
                physicalDamagePenetration = 0f;
            }
            this.physicalDamagePenetration = _physicalDamagePenetration;
            if (magicPenetration < 0f)
            {
                magicPenetration = 0f;
            }
            this.magicPenetration = _magicPenetration;
            if (lifeStealPercentage < 0f)
            {
                lifeStealPercentage = 0f;
            }
            else if (lifeStealPercentage > 1f)
            {
                lifeStealPercentage = 1f;
            }
            this.lifeStealPercentage = _lifeStealPercentage;
            /*
            this.physicalDamageOverTime = _physicalDamageOverTime;
            this.magicDamageOverTime = _magicDamageOverTime;
            this.trueDamageOverTime = _trueDamageOverTime;
            */
        }
    }
}