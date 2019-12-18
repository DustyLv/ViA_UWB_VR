#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (ParametricRoom))]
public class ParametricRoomEditor : Editor {

    public override void OnInspectorGUI () {

        DrawDefaultInspector ();

        ParametricRoom myScript = (ParametricRoom) target;
        if (GUILayout.Button ("Build Room")) {
            myScript.BuildRoom ();
        }
    }

}
#endif