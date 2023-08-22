using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace HTNWIC.Dummy
{
    public class DummyHealthBar : NetworkBehaviour
    {
        [SerializeField]
        private Canvas healthBarCanvas;

        [SerializeField]
        private Image healthBar;

        private void Start()
        {
            healthBar.fillAmount = 1f;
        }

        private void Update()
        {
            // make sure the health bar is always facing the camera
            Transform targetCamera = Camera.allCameras[0].transform;
            Vector3 targetPosition = new Vector3(targetCamera.position.x, healthBarCanvas.transform.position.y, targetCamera.position.z);
            healthBarCanvas.transform.LookAt(targetPosition);
        }

        [Server]
        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
            UpdateHealthBarRpc(currentHealth, maxHealth);
        }

        [ClientRpc]
        public void UpdateHealthBarRpc(float currentHealth, float maxHealth)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
