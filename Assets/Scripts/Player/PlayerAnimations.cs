using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NetworkAnimator))]
    public class PlayerAnimations : NetworkBehaviour
    {
        private NetworkAnimator networkAnimator;

        private void Start()
        {
            if (!isLocalPlayer) return;
            networkAnimator = GetComponent<NetworkAnimator>();
            if (networkAnimator == null)
            {
                Debug.LogError("NetworkAnimator component not found on this object");
            }
        }

        public void PlayRunAnimation()
        {
            networkAnimator.SetTrigger("Run");
        }

        public void PlayIdleAnimation()
        {
            networkAnimator.SetTrigger("Idle");
        }
    }
}
