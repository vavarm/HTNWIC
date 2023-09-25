using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HTNWIC.AI
{
    [CustomEditor(typeof(AIController), true)]
    public class AIControllerEditor : Editor
    {
        public void OnSceneGUI()
        {
            AIController linkedObject = (AIController)target;
            Handles.color = Color.red;
            if (!linkedObject.DrawGizmos) return;
            // Draw the min radius arc
            Handles.DrawWireArc(linkedObject.transform.position, linkedObject.transform.up, linkedObject.transform.forward, linkedObject.TotalFOV / 2, linkedObject.MinRadius);
            Handles.DrawWireArc(linkedObject.transform.position, linkedObject.transform.up, linkedObject.transform.forward, -linkedObject.TotalFOV / 2, linkedObject.MinRadius);
            // Draw the max radius arc
            Handles.DrawWireArc(linkedObject.transform.position, linkedObject.transform.up, linkedObject.transform.forward, linkedObject.TotalFOV / 2, linkedObject.MaxRadius);
            Handles.DrawWireArc(linkedObject.transform.position, linkedObject.transform.up, linkedObject.transform.forward, -linkedObject.TotalFOV / 2, linkedObject.MaxRadius);
            // Draw the lines connecting the arcs
            Quaternion leftAngle = linkedObject.transform.rotation * Quaternion.AngleAxis(-linkedObject.TotalFOV / 2, Vector3.up);
            Quaternion rightAngle = linkedObject.transform.rotation * Quaternion.AngleAxis(linkedObject.TotalFOV / 2, Vector3.up);
            Ray leftRay = new Ray(linkedObject.transform.position, leftAngle * Vector3.forward);
            Ray rightRay = new Ray(linkedObject.transform.position, rightAngle * Vector3.forward);
            Handles.DrawLine(leftRay.GetPoint(linkedObject.MinRadius), leftRay.GetPoint(linkedObject.MaxRadius));
            Handles.DrawLine(rightRay.GetPoint(linkedObject.MinRadius), rightRay.GetPoint(linkedObject.MaxRadius));
        }
    }
}
