using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTNWIC {
    public class InteractableFX : MonoBehaviour, IInteractableFX
    {
        [SerializeField]
        private ParticleSystem interactionFX;
        public ParticleSystem InteractionFX => interactionFX;

        private void OnEnable()
        {
            if (interactionFX == null)
            {
                Debug.LogError("No particle system assigned to " + gameObject.name);
            }
        }
    }
}

