using System;
using System.Collections;
using System.Collections.Generic;
using HTNWIC.Items;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerAnimations))]
    [RequireComponent(typeof(WeaponManager))]
    public class PlayerController : NetworkBehaviour
    {
        private PlayerMotor playerMotor;
        private PlayerAnimations playerAnimations;
        private WeaponManager weaponManager;

        private Vector2 move;

        public bool isMoving { get; private set; } = false;

        public bool isAttacking { get; private set; } = false;

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                if (weaponManager.CurrentWeapon == null || isAttacking) return;
                StartCoroutine(Attack());
            }
        }

        private void Start()
        {
            if (!isLocalPlayer) return;
            playerMotor = GetComponent<PlayerMotor>();
            playerAnimations = GetComponent<PlayerAnimations>();
            weaponManager = GetComponent<WeaponManager>();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            Vector3 movementDirection = new Vector3(move.x, 0f, move.y);
            movementDirection.Normalize();

            playerMotor.Move(movementDirection);

            playerMotor.Rotate(movementDirection);

            if (move != Vector2.zero)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        IEnumerator Attack() {
            isAttacking = true;
            Debug.Log("Attack");
            switch (weaponManager.CurrentWeapon.type)
            {
                case WeaponType.OneHanded:
                    playerAnimations.PlayAttackOHAnimation();
                    break;
                default:
                    goto case WeaponType.OneHanded;
            }
            yield return new WaitForSeconds(0.84f);
            Debug.Log("End of Attack");
            isAttacking = false;
        }
    }
}
