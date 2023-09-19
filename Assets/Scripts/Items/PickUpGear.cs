using FishNet.Managing.Logging;
using HTNWIC;
using HTNWIC.Items;
using HTNWIC.Player;
using UnityEngine;
using FishNet.Object;

public class PickUpGear : NetworkBehaviour, IInteractable
{
    [SerializeField]
    private Gear gear;
    public Gear Gear => gear;

    [SerializeField]
    private string interactionPrompt = "Pick up gear";
    public string InteractionPrompt => interactionPrompt;

    public void Interact(GameObject source)
    {
        CmdEquipGear(source);
    }

    [ServerRpc(RequireOwnership = false)]
    private void CmdEquipGear(GameObject source)
    {
        // equip the gear on the server
        if (source == null)
        {
            Debug.LogError("PickUpGear: source is null");
            return;
        }
        if (source.GetComponent<GearManager>() == null)
        {
            Debug.LogError("PickUpGear: source does not have GearManager");
            return;
        }
        GearManager gm = source.GetComponent<GearManager>();
        gm.EquipGear(gear);
        // equip the Gear on all clients
        RpcEquipGear(source);
        // destroy this object on all instances
        base.Despawn();
    }

    [ObserversRpc]
    private void RpcEquipGear(GameObject source)
    {
        if (source == null)
        {
            Debug.LogError("PickUpGear: source is null");
            return;
        }
        if (source.GetComponent<GearManager>() == null)
        {
            Debug.LogError("PickUpGear: source does not have GearManager");
            return;
        }
        GearManager gm = source.GetComponent<GearManager>();
        gm.EquipGear(gear);
    }
}
