using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : NetworkBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        private float moveSpeed = 10f;

        private void Start()
        {
            if (!isLocalPlayer) return;
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 movementDirection)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.deltaTime);
        }

        public void Rotate(Vector3 movementDirection)
        {
            if (movementDirection != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.LookRotation(movementDirection));
            }
        }
    }
}
