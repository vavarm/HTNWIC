using UnityEngine;
using FishNet.Object;
using HTNWIC.Items;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(WeaponManager))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimations : NetworkBehaviour
    {
        private Animator animator;
        private WeaponManager weaponManager;
        private PlayerController playerController;

        public bool isMovingAnimation = false;
        public bool isAttackingAnimation = false;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            animator = GetComponent<Animator>();
            weaponManager = GetComponent<WeaponManager>();
            playerController = GetComponent<PlayerController>();
        }
        
        public override void OnStartClient()
        {
            base.OnStartClient();
            animator = GetComponent<Animator>();
            weaponManager = GetComponent<WeaponManager>();
            playerController = GetComponent<PlayerController>();
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
