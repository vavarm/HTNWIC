using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void Start()
        {
            // modify the radius of the particle system to match the interaction range
            ParticleSystem.ShapeModule psShape = InteractionFX.shape;
            psShape.radius = GameSettings.interactionRange;
        }
    }
}

