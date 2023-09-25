using System.Collections;
using System.Collections.Generic;
using FishNet.Demo.AdditiveScenes;
using UnityEngine;

namespace HTNWIC.AI
{
    public class AIControllerDemo : AIController
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentWaypoint = GetRandomPositionInPieSlice();
                Debug.Log($"New waypoint: {currentWaypoint}");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!DrawGizmos) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentWaypoint, 0.2f);
        }
    }
}
