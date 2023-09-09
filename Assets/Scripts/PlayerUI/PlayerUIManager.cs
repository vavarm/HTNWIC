using UnityEngine;
using TMPro;
using UnityEngine.UI;
using HTNWIC.Player;

namespace HTNWIC.PlayerUI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField]
        private GameObject interactionPanel;
        [SerializeField]
        private Button interactionButton;
        [SerializeField]
        private TextMeshProUGUI interactionTextPlaceholder;

        private GameObject player;

        public void EnableInteractionPanel(bool isEnabled, string interactionText)
        {
            interactionPanel.SetActive(isEnabled);
            interactionTextPlaceholder.text = interactionText;
        }

        public void SetPlayer(GameObject player)
        {
            // Set the player
            this.player = player;
            // add a listener to the interaction button
            interactionButton.onClick.AddListener(() => player.GetComponent<Interactor>().Interact());
        }
    }
}
