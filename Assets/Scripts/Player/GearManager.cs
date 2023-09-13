using System.Collections;
using System.Collections.Generic;
using HTNWIC.Items;
using Mirror;
using UnityEngine;

namespace HTNWIC.Player
{
    public class GearManager: NetworkBehaviour
    {
        private Gear currentGear;
        public Gear CurrentGear => currentGear;

        private GameObject gearInstance;

        [SerializeField]
        private Transform gearHolder;

        public void EquipGear(Gear gear)
        {
            // destroy previous gear
            if (gearInstance != null)
            {
                Destroy(gearInstance);
            }
            // equip new gear
            currentGear = gear;
            gearInstance = Instantiate(gear.prefab, gearHolder);
        }
    }
}
