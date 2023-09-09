using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HTNWIC.PlayerUI;
using System.ComponentModel;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(PlayerSetup))]
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private Transform interactionPoint;
        [SerializeField]
        private float interactionRange = 2f;
        [SerializeField]
        private LayerMask interactionLayerMask;

        private Collider[] interactionResults = new Collider[10];
        private Collider currentInteractionResult;
        private int interactionResultsCount;

        private PlayerSetup playerSetup;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (currentInteractionResult != null) {
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
        }

        void Start()
        {
            playerSetup = GetComponent<PlayerSetup>();
        }

        void Update()
        {
            interactionResultsCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, interactionResults, interactionLayerMask);
            if (interactionResultsCount > 0)
            {
                // reset the current interaction result if it's not the same as the first result
                if(currentInteractionResult != null && currentInteractionResult != interactionResults[0])
                    RemoveCurrentInteractableObject();
                // set the new current interaction result
                currentInteractionResult = interactionResults[0];
                if (currentInteractionResult.TryGetComponent(out IInteractable interactable))
                {
                    // Show interaction UI
                    if (playerSetup.playerUIInstance != null)
                    {
                        playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(true, interactable.InteractionPrompt);
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
                    playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(false, "");
                }
                // disable FX on the object and reset the current interaction result
                RemoveCurrentInteractableObject();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
        }

        private void EnableCurrentInteractableObjectFX()
        {
            if(currentInteractionResult != null && currentInteractionResult.TryGetComponent(out IInteractableFX component))
            {
                // modify the radius of the particle system to match the interaction range
                ParticleSystem ps = component.InteractionFX.GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule psShape = ps.shape;
                psShape.radius = interactionRange;
                // enable FX on the object
                component.InteractionFX.gameObject.SetActive(true);
            }
            
        }

        private void RemoveCurrentInteractableObject()
        {
            if (currentInteractionResult != null && currentInteractionResult.TryGetComponent(out IInteractableFX component))
            {
                // disable FX on the object
                component.InteractionFX.gameObject.SetActive(false);
            }
            // reset the current interaction result
            currentInteractionResult = null;
        }

        private void OnDisable()
        {
            // Hide interaction UI
            if (playerSetup.playerUIInstance != null)
            {
                playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(false, "");
            }
            // disable FX on the object and reset the current interaction result
            RemoveCurrentInteractableObject();
        }
    }
}
