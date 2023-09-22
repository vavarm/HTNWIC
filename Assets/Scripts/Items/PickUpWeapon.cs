using HTNWIC;
using HTNWIC.Items;
using HTNWIC.Player;
using UnityEngine;
using FishNet.Object;

public class PickUpWeapon : NetworkBehaviour, IInteractable
{
    [SerializeField]
    private Weapon weapon;
    public Weapon Weapon => weapon;

    [SerializeField]
    private string interactionPrompt = "Pick up weapon";
    public string InteractionPrompt => interactionPrompt;

    public void Interact(GameObject source)
    {
        CmdEquipWeapon(source);
    }

    [ServerRpc(RequireOwnership = false)]
    private void CmdEquipWeapon(GameObject source)
    {
        // equip the weapon on the server
        if (source == null)
        {
            Debug.LogError("PickUpWeapon: source is null");
            return;
        }
        if (source.GetComponent<WeaponManager>() == null)
        {
            Debug.LogError("PickUpWeapon: source does not have WeaponManager");
            return;
        }
        WeaponManager wm = source.GetComponent<WeaponManager>();
        wm.EquipWeapon(weapon);
        // destroy this object on all instances
        base.Despawn();
    }
}
