using HTNWIC.Items;
using UnityEngine;

namespace HTNWIC
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }

        [SerializeField]
        private Weapon[] weapons;

        [SerializeField]
        private Gear[] gears;

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("There can only be one GameData instance!", this);
            }
            Instance = this;
        }

        public Weapon GetWeapon(int index)
        {
            return weapons[index];
        }

        public int GetWeaponCount()
        {
            return weapons.Length;
        }

        public Gear GetGear(int index)
        {
            return gears[index];
        }

        public int GetWeaponIndexByName(string name)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetGearIndexByName(string name)
        {
            for (int i = 0; i < gears.Length; i++)
            {
                if (gears[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
