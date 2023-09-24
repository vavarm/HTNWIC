using UnityEngine;
using UnityEngine.UI;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace HTNWIC.Dummy
{
    public class DummyHealthBar : NetworkBehaviour
    {
        [SerializeField]
        private Canvas healthBarCanvas;

        [SerializeField]
        private Image healthBar;
        [SyncVar(OnChange = nameof(UpdateHealthBar))]
        private float healthPercentage;

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
        public void SetHealthBarValue(float currentHealth, float maxHealth)
        {
            healthPercentage = currentHealth / maxHealth;
            Debug.Log("SetHealthBarValue: " + healthPercentage);
        }

        private void UpdateHealthBar(float oldHealthPercentage, float newHealthPercentage, bool asServer)
        {
            healthBar.fillAmount = newHealthPercentage;
            Debug.Log("UpdateHealthBar: " + newHealthPercentage);
        }
    }
}
