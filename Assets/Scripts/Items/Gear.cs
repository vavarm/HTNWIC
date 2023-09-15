using UnityEditor;
using UnityEngine;


namespace HTNWIC.Items
{
    public enum GearType
    {
        Head,
        Shoulders,
        Gloves,
        Chest,
        Legs,
        Feet
    }

    [CreateAssetMenu(fileName = "NewGear", menuName = "HTNWIC/Gear")]

    public class Gear : Item
    {
        public string gearName;
        public GameObject prefab;
        public GearType type;
        public float bonusHealth;
        public float armor;
        public float magicalResistance;
    }
}

