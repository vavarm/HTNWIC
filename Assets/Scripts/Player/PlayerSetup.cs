using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using HTNWIC.Camera;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    private Camera sceneCamera;

    [SerializeField]
    private Behaviour[] componentsToDisable;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            // Disable all components that should only be active on the player that we control
            DisableComponents();
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
    }
}
