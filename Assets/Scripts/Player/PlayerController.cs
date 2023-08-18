using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerAnimations))]
    public class PlayerController : NetworkBehaviour
    {
        private PlayerMotor playerMotor;
        private PlayerAnimations playerAnimations;

        private Vector2 move;

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();

        }

        private void Start()
        {
            if (!isLocalPlayer) return;
            playerMotor = GetComponent<PlayerMotor>();
            playerAnimations = GetComponent<PlayerAnimations>();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            Vector3 movementDirection = new Vector3(move.x, 0f, move.y);
            movementDirection.Normalize();

            playerMotor.Move(movementDirection);

            playerMotor.Rotate(movementDirection);

            if (move == Vector2.zero)
            {
                playerAnimations.PlayIdleAnimation();
            }
            else
            {
                playerAnimations.PlayRunAnimation();
            }
        }

    }
}
