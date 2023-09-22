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
        [Header("Attacker Identity")]
        private GameObject attacker;
        public GameObject Attacker => attacker;
        [Header("Damage Amount")]
        private float physicalDamageAmount = 0f;
        public float PhysicalDamageAmount => physicalDamageAmount;
        private float magicalDamageAmount = 0f;
        public float MagicalDamageAmount => magicalDamageAmount;
        private float trueDamageAmount = 0f;
        public float TrueDamageAmount => trueDamageAmount;
        [Header("Additional Damage Data")]
        private float physicalDamagePenetration = 0f;
        public float PhysicalDamagePenetration => physicalDamagePenetration;
        private float magicalPenetration = 0f;
        public float MagicalPenetration => magicalPenetration;
        [Header("Lifesteal")]
        private float lifeStealPercentage = 0f;
        public float LifeStealPercentage => lifeStealPercentage;
        /*
        [Header("Damage Over Time")]
        private DamageOverTimeData physicalDamageOverTime;
        public DamageOverTimeData PhysicalDamageOverTime => physicalDamageOverTime;
        private DamageOverTimeData magicalDamageOverTime;
        public DamageOverTimeData MagicalDamageOverTime => magicalDamageOverTime;
        private DamageOverTimeData trueDamageOverTime;
        public DamageOverTimeData TrueDamageOverTime => trueDamageOverTime;
        */

        public DamageData(GameObject _attacker, float _physicalDamageAmount, float _magicalDamageAmount, float _trueDamageAmount, float _physicalDamagePenetration, float _magicalPenetration, float _lifeStealPercentage, DamageOverTimeData? _physicalDamageOverTime = null, DamageOverTimeData? _magicalDamageOverTime = null, DamageOverTimeData? _trueDamageOverTime = null)
        {
            this.attacker = _attacker;
            if (physicalDamageAmount < 0f)
            {
                physicalDamageAmount = 0f;
            }
            this.physicalDamageAmount = _physicalDamageAmount;
            if (magicalDamageAmount < 0f)
            {
                magicalDamageAmount = 0f;
            }
            this.magicalDamageAmount = _magicalDamageAmount;
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
            if (magicalPenetration < 0f)
            {
                magicalPenetration = 0f;
            }
            this.magicalPenetration = _magicalPenetration;
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
            this.magicalDamageOverTime = _magicalDamageOverTime;
            this.trueDamageOverTime = _trueDamageOverTime;
            */
        }
    }
}