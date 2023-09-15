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
            Transform targetCamera;
            if (Camera.main != null)
            {
                targetCamera = Camera.main.transform;
            }
            else
            {
                targetCamera = GameObject.FindGameObjectsWithTag("PlayerCamera")[0].transform;
            }
            healthBarCanvas.transform.LookAt(healthBarCanvas.transform.position + targetCamera.rotation * Vector3.forward, targetCamera.rotation * Vector3.up);
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
