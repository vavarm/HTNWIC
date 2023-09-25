using HTNWIC.AI;
using UnityEngine;

namespace HTNWIC.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyController))]
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator animator;
        private EnemyController controller;

        [SerializeField]
        private string idleAnimationName = "Idle";
        [SerializeField]
        private string sitAnimationName = "Sit";
        [SerializeField]
        private string walkForwardAnimationName = "WalkForward";
        [SerializeField]
        private string runForwardAnimationName = "Run Forward";
        [SerializeField]
        private string attack1AnimationName = "Attack1";

        private void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<EnemyController>();
        }

        void Update()
        {
            switch (controller.State)
            {
                case EnemyController.EnemyState.Idle:
                    animator.SetBool(idleAnimationName, true);
                    animator.SetBool(sitAnimationName, false);
                    animator.SetBool(walkForwardAnimationName, false);
                    animator.SetBool(runForwardAnimationName, false);
                    break;
                case EnemyController.EnemyState.Sit:
                    animator.SetBool(idleAnimationName, false);
                    animator.SetBool(sitAnimationName, true);
                    animator.SetBool(walkForwardAnimationName, false);
                    animator.SetBool(runForwardAnimationName, false);
                    break;
                case EnemyController.EnemyState.Patrol:
                    animator.SetBool(idleAnimationName, false);
                    animator.SetBool(sitAnimationName, false);
                    animator.SetBool(walkForwardAnimationName, true);
                    animator.SetBool(runForwardAnimationName, false);
                    break;
                case EnemyController.EnemyState.Chase:
                    animator.SetBool(idleAnimationName, false);
                    animator.SetBool(sitAnimationName, false);
                    animator.SetBool(walkForwardAnimationName, false);
                    animator.SetBool(runForwardAnimationName, true);
                    break;
            }
        }
    }
}
