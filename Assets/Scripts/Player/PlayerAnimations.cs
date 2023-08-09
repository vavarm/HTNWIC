using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayRunAnimation()
        {
            animator.SetTrigger("Run");
        }

        public void PlayIdleAnimation()
        {
            animator.SetTrigger("Idle");
        }
    }
}
