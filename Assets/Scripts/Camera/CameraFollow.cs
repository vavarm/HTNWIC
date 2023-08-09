using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTNWIC.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField]
        private float offsetHeight = 10f;
        [SerializeField]
        private float offsetBackward = 14f;


        private void Update()
        {
            if (target != null)
            {
                Vector3 targetPosition = target.position + new Vector3(0f, offsetHeight, -offsetBackward);
                transform.position = targetPosition;
                transform.LookAt(target);
            }
        }
    }
}
