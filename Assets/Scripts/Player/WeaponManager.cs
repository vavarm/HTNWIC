using HTNWIC.Items;
using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

namespace HTNWIC.Player
{
    public class WeaponManager: NetworkBehaviour
    {
        [SyncVar(OnChange = nameof(OnWeaponChange))]
        private int currentWeaponIndex = -1;

        private Weapon currentWeapon;
        public Weapon CurrentWeapon => currentWeapon;

        private GameObject weaponInstance;

        [SerializeField]
        private Transform weaponHolder;

        [Server]
        public void EquipWeapon(Weapon weapon)
        {
            currentWeaponIndex = GameData.Instance.GetWeaponIndexByName(weapon.name);
        }

        private void OnWeaponChange(int oldWeaponIndex, int newWeaponIndex, bool asServer)
        {
            if(newWeaponIndex < 0 || newWeaponIndex >= GameData.Instance.GetWeaponCount())
            {
                Debug.LogError("WeaponManager: newWeaponIndex out of range");
                return;
            }
            if (weaponInstance != null)
            {
                Destroy(weaponInstance);
            }
            currentWeapon = GameData.Instance.GetWeapon(newWeaponIndex);
            weaponInstance = Instantiate(currentWeapon.prefab, weaponHolder);
        }

    }
}
