using HTNWIC.Items;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace HTNWIC.Player
{
    public class GearManager: NetworkBehaviour
    {
        [SyncVar(OnChange = nameof(OnGearChange))]
        private int currentGearIndex = -1;
        
        private Gear currentGear;
        public Gear CurrentGear => currentGear;

        private GameObject gearInstance;

        [SerializeField]
        private Transform gearHolder;

        [Server]
        public void EquipGear(Gear gear)
        {
            currentGearIndex = GameData.Instance.GetGearIndexByName(gear.name);
            Debug.Log("GearManager: EquipGear: " + gear.name);
        }
        
        private void OnGearChange(int oldGearIndex, int newGearIndex, bool asServer)
        {
            if(newGearIndex < 0 || newGearIndex >= GameData.Instance.GetGearCount())
            {
                Debug.LogError("GearManager: newGearIndex out of range");
                return;
            }
            if (gearInstance != null)
            {
                Destroy(gearInstance);
                Debug.Log("GearManager: Destroyed gearInstance");
            }
            currentGear = GameData.Instance.GetGear(newGearIndex);
            gearInstance = Instantiate(currentGear.prefab, gearHolder);
        }
    }
}
