using UnityEngine;
using FishNet.Object;

namespace HTNWIC.AI
{
    public abstract class AIController : NetworkBehaviour
    {
        [Header("Move Cone Settings")]
        [SerializeField]
        public bool DrawGizmos = false;
        [SerializeField]
        protected float minRadius = 2;
        public float MinRadius => minRadius;
        [SerializeField]
        protected float maxRadius = 5;
        public float MaxRadius => maxRadius;
        [SerializeField]
        protected float totalFOV = 60f;
        public float TotalFOV => totalFOV;

        protected Vector3 currentWaypoint = Vector3.zero;

        protected Vector3 GetRandomPositionInPieSlice()
        {
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(-totalFOV / 2, totalFOV / 2), transform.up);

            Vector3 direction = rotation * transform.forward;

            float distance = Random.Range(minRadius, maxRadius);

            return transform.position + direction * distance;
        }
    }
}
