using UnityEngine;
using FishNet.Object;
using FishNet.Component.Animating;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NetworkAnimator))]
    [RequireComponent(typeof(WeaponManager))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimations : NetworkBehaviour
    {
        private Animator animator;
        private NetworkAnimator networkAnimator;
        private WeaponManager weaponManager;
        private PlayerController playerController;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!base.IsOwner) return;
            animator = GetComponent<Animator>();
            networkAnimator = GetComponent<NetworkAnimator>();
            weaponManager = GetComponent<WeaponManager>();
            playerController = GetComponent<PlayerController>();
            if (animator == null)
            {
                Debug.LogError("animator component not found on this object");
            }
        }

        public void Update()
        {
            if (!playerController.isMoving && weaponManager.CurrentWeapon == null)
            {
                PlayIdleAnimation();
            }
            else if (playerController.isMoving && weaponManager.CurrentWeapon == null)
            {
                PlayRunAnimation();
            }
            else if (!playerController.isMoving && weaponManager.CurrentWeapon != null)
            {
                PlayIdleOHAnimation();
            }
            else if (playerController.isMoving && weaponManager.CurrentWeapon != null)
            {
                PlayRunOHAnimation();
            }
        }

        private void PlayRunAnimation()
        {
            networkAnimator.SetTrigger("Run");
        }

        private void PlayRunOHAnimation()
        {
            networkAnimator.SetTrigger("RunOH");
        }

        private void PlayIdleAnimation()
        {
            networkAnimator.SetTrigger("Idle");
        }

        private void PlayIdleOHAnimation()
        {
            networkAnimator.SetTrigger("IdleOH");
        }

        public void PlayAttackOHAnimation()
        {
            networkAnimator.SetTrigger("AttackOH");
        }
    }
}
