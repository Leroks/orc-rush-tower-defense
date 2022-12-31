using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI()
    {

        Handles.color = Color.red;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            UnityEngine.Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            UnityEngine.Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, UnityEngine.Quaternion.identity, 0.5f, UnityEngine.Vector3.zero, Handles.SphereHandleCap);

            GUIStyle textstyle = new GUIStyle();
            textstyle.fontStyle = FontStyle.Bold;
            textstyle.fontSize = 16;
            textstyle.normal.textColor = Color.yellow;
            UnityEngine.Vector3 textAllignment = UnityEngine.Vector3.down * 0.35f + UnityEngine.Vector3.right * 0.35f;

            Handles.Label(currentWaypointPoint + textAllignment, (i + 1).ToString(), textstyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move Waypoint");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }
    }
}
