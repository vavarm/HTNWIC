using UnityEngine;
using UnityEngine.InputSystem;
using HTNWIC.PlayerUI;
using FishNet.Object;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(PlayerSetup))]
    public class Interactor : NetworkBehaviour
    {
        [SerializeField]
        private Transform interactionPoint;
        [SerializeField]
        private float interactionRange;
        [SerializeField]
        private LayerMask interactionLayerMask;

        private Collider[] interactionResults = new Collider[10];
        private Collider currentInteractionResult;
        private int interactionResultsCount;

        private PlayerSetup playerSetup;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!base.IsOwner) return;
            if (context.performed)
            {
                Interact();
            }
        }

        public void Interact()
        {
            if (currentInteractionResult != null)
            {
                if (interactionResults[0].TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(gameObject);
                }
                else
                {
                    Debug.Log("No interactable component found on " + interactionResults[0].name);
                }
            }
        }

        void Start()
        {
            playerSetup = GetComponent<PlayerSetup>();
            interactionRange = GameSettings.interactionRange;
        }

        void Update()
        {
            if (!base.IsOwner) return;
            interactionResultsCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, interactionResults, interactionLayerMask);
            // order the results by distance to the interaction point (closest first) (if an object is null, it will be placed at the end of the array)
            System.Array.Sort(interactionResults, (x, y) => (x == null ? 1 : (y == null ? -1 : (x.transform.position - interactionPoint.position).sqrMagnitude.CompareTo((y.transform.position - interactionPoint.position).sqrMagnitude))));
            if (interactionResultsCount > 0)
            {
                // reset the current interaction result if it's not the same as the first result
                if(currentInteractionResult != null && currentInteractionResult != interactionResults[0])
                    DisableCurrentInteractableObjectFX();
                // set the new current interaction result
                currentInteractionResult = interactionResults[0];
                if (currentInteractionResult.TryGetComponent(out IInteractable interactable))
                {
                    // Show interaction UI
                    if (playerSetup.playerUIInstance != null)
                    {
                        if(currentInteractionResult.TryGetComponent(out PickUpWeapon pickUpWeapon))
                        {
                            playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(true, interactable.InteractionPrompt, pickUpWeapon.Weapon.icon);
                        }
                        else if(currentInteractionResult.TryGetComponent(out PickUpGear pickUpGear))
                        {
                            playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(true, interactable.InteractionPrompt, pickUpGear.Gear.icon);
                        }
                        else
                            playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(true, interactable.InteractionPrompt, null);
                    }
                    // enable FX on the object
                    EnableCurrentInteractableObjectFX();
                }
            }
            else
            {
                // Hide interaction UI
                if (playerSetup.playerUIInstance != null)
                {
                    playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(false, "", null);
                }
                // disable FX on the object and reset the current interaction result
                DisableCurrentInteractableObjectFX();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
        }

        private void EnableCurrentInteractableObjectFX()
        {
            if(currentInteractionResult != null && currentInteractionResult.TryGetComponent(out InteractableIndicator component))
            {
                // enable FX on the object
                component.DisplayMeshRenderer(true);
            }
            
        }

        private void DisableCurrentInteractableObjectFX()
        {
            if (currentInteractionResult != null && currentInteractionResult.TryGetComponent(out InteractableIndicator component))
            {
                // disable FX on the object
                component.DisplayMeshRenderer(false);
            }
            // reset the current interaction result
            currentInteractionResult = null;
        }

        private void OnDisable()
        {
            if(!base.IsOwner) return;
            // Hide interaction UI
            if (playerSetup.playerUIInstance != null)
            {
                playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(false, "", null);
            }
            // disable FX on the object and reset the current interaction result
            DisableCurrentInteractableObjectFX();
        }
    }
}
