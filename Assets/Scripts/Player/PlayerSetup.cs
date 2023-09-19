using UnityEngine;
using Mirror;
using HTNWIC.PlayerCamera;
using HTNWIC.PlayerUI;
using HTNWIC.Utils;

namespace HTNWIC.Player
{
    public class PlayerSetup : NetworkBehaviour
    {
        [SerializeField]
        private Camera playerCamera;
        private Camera sceneCamera;

        public Camera PlayerCamera => playerCamera;

        [SerializeField]
        private GameObject playerUIPrefab;

        public GameObject playerUIInstance { get; private set; }

        /*
        [SerializeField]
        private string localPlayerLayerName = "LocalPlayer";
        */
        [SerializeField]
        private string remotePlayerLayerName = "RemotePlayer";

        [SerializeField]
        private Behaviour[] componentsToDisable;

        private void Start()
        {
            if (!isLocalPlayer)
            {
                // Disable all components that should only be active on the player that we control
                DisableComponents();
                // Assign the player to the remote player layer
                LayerUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer(remotePlayerLayerName));
            }
            else
            {
                // Instantiate the camera to the root of the hierarchy
                playerCamera = Instantiate(playerCamera);
                // Set the camera's target to the player
                playerCamera.GetComponent<CameraFollow>().Target = transform;
                // Disable the scene camera
                sceneCamera = Camera.main;
                if (sceneCamera != null)
                {
                    sceneCamera.gameObject.SetActive(false);
                }
                // Create PlayerUI
                playerUIInstance = Instantiate(playerUIPrefab);
                // Set the playerUI's player to this player
                playerUIInstance.GetComponent<PlayerUIManager>().SetPlayer(gameObject);
            }
        }

        private void DisableComponents()
        {
            foreach (Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }
        }
        private void OnDisable()
        {
            if (!isLocalPlayer) return;
            // Re-enable the scene camera
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(true);
            }
            // Destroy the player camera
            if (playerCamera != null)
            {
                Destroy(playerCamera.gameObject);
            }
            // Destroy the player UI
            if (playerUIInstance != null)
            {
                Destroy(playerUIInstance);
            }
        }
    }
}
