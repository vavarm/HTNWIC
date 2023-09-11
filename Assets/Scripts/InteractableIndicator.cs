using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace HTNWIC {
    public class InteractableIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObject interactionIndicator;
        public GameObject InteractionIndicator => interactionIndicator;

        private void OnEnable()
        {
            if (interactionIndicator == null)
            {
                Debug.LogError("No interaction indicator assigned to " + gameObject.name);
            }
        }

        private void Start()
        {
            // modify the size of the interaction indicator to match the interaction range of the player
            interactionIndicator.transform.localScale = new Vector3(GameSettings.interactionRange, 1, GameSettings.interactionRange);
        }
    }
}

