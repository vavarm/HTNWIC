using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HTNWIC.PlayerUI;

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
        private int interactionResultsCount;

        private PlayerSetup playerSetup;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (interactionResults[0] != null) {
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
                if (interactionResults[0].TryGetComponent(out IInteractable interactable))
                {
                    // Show interaction UI
                    if (playerSetup.playerUIInstance != null)
                    {
                        playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(true, interactable.InteractionPrompt);
                    }
                    // TODO: enable UI around the object
                }
            }
            else
            {
                // Hide interaction UI
                if (playerSetup.playerUIInstance != null)
                {
                    playerSetup.playerUIInstance.GetComponent<PlayerUIManager>().EnableInteractionPanel(false, "");
                }
                // TODO: disable UI around the object
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
        }
    }
}
