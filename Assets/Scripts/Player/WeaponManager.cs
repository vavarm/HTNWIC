using HTNWIC.Items;
using FishNet.Object;
using UnityEngine;

namespace HTNWIC.Player
{
    public class WeaponManager: NetworkBehaviour
    {
        private Weapon currentWeapon;
        public Weapon CurrentWeapon => currentWeapon;

        private GameObject weaponInstance;

        [SerializeField]
        private Transform weaponHolder;

        public void EquipWeapon(Weapon weapon)
        {
            // destroy previous weapon
            if (weaponInstance != null)
            {
                Destroy(weaponInstance);
            }
            // equip new weapon
            currentWeapon = weapon;
            weaponInstance = Instantiate(weapon.prefab, weaponHolder);
        }
    }
}
