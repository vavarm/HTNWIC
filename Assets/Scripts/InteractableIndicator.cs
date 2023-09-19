using UnityEngine;

namespace HTNWIC {
    public class InteractableIndicator : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer interactionIndicatorRenderer;

        private void OnEnable()
        {
            if (interactionIndicatorRenderer == null)
            {
                Debug.LogError("No interaction indicator assigned to " + gameObject.name);
            }
        }

        public void DisplayMeshRenderer(bool display)
        {
            if (interactionIndicatorRenderer != null)
            {
                interactionIndicatorRenderer.enabled = display;
            }
        }

        private void Start()
        {
            // modify the size of the interaction indicator to match the interaction range of the player
            interactionIndicatorRenderer.transform.localScale =
                new Vector3(GameSettings.interactionRange, 1, GameSettings.interactionRange);
        }
    }
}

