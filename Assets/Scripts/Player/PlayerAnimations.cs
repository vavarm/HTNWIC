using FishNet.Object;
using UnityEngine;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(WeaponManager))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimations : NetworkBehaviour
    {
        private Animator animator;
        private WeaponManager weaponManager;
        private PlayerController playerController;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!base.IsOwner) return;
            animator = GetComponent<Animator>();
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
            animator.SetTrigger("Run");
        }

        private void PlayRunOHAnimation()
        {
            animator.SetTrigger("RunOH");
        }

        private void PlayIdleAnimation()
        {
            animator.SetTrigger("Idle");
        }

        private void PlayIdleOHAnimation()
        {
            animator.SetTrigger("IdleOH");
        }

        public void PlayAttackOHAnimation()
        {
            animator.SetTrigger("AttackOH");
        }
    }
}
