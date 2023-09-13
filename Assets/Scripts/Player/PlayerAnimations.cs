using System.Collections;
using System.Collections.Generic;
using HTNWIC.Player;
using Mirror;
using UnityEngine;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NetworkAnimator))]
    [RequireComponent(typeof(WeaponManager))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimations : NetworkBehaviour
    {
        private NetworkAnimator networkAnimator;
        private WeaponManager weaponManager;
        private PlayerController playerController;

        private void Start()
        {
            if (!isLocalPlayer) return;
            networkAnimator = GetComponent<NetworkAnimator>();
            weaponManager = GetComponent<WeaponManager>();
            playerController = GetComponent<PlayerController>();
            if (networkAnimator == null)
            {
                Debug.LogError("NetworkAnimator component not found on this object");
            }
        }

        public void Update()
        {
            if (!playerController.isMoving & weaponManager.CurrentWeapon == null)
            {
                PlayIdleAnimation();
            }
            else if (playerController.isMoving & weaponManager.CurrentWeapon == null)
            {
                PlayRunAnimation();
            }
            else if (!playerController.isMoving & weaponManager.CurrentWeapon != null)
            {
                PlayIdleOHAnimation();
            }
            else if (playerController.isMoving & weaponManager.CurrentWeapon != null)
            {
                PlayRunOHAnimation();
            }
        }

        public void PlayRunAnimation()
        {
            networkAnimator.SetTrigger("Run");
        }

        public void PlayRunOHAnimation()
        {
            networkAnimator.SetTrigger("RunOH");
        }

        public void PlayIdleAnimation()
        {
            networkAnimator.SetTrigger("Idle");
        }

        public void PlayIdleOHAnimation()
        {
            networkAnimator.SetTrigger("IdleOH");
        }
    }
}
