using UnityEditor;
using UnityEngine;

namespace HTNWIC.Items
{
    public enum WeaponType
    {
        OneHanded,
        TwoHanded
    }

    [CreateAssetMenu(fileName = "NewWeapon", menuName = "HTNWIC/Weapon")]
    public class Weapon: Item
    {
        public string weaponName;
        public GameObject prefab;
        public WeaponType type;
        public float physicalDamage;
        public float magicalDamage;
        public float trueDamage;
    }
}
