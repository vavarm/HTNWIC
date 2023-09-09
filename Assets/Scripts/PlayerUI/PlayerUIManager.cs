using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HTNWIC.PlayerUI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject interactionPanel;
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
            this.player = player;
        }
    }
}
