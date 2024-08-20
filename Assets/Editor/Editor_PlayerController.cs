using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BW
{
    [CustomEditor(typeof(PlayerController))]
    public class Editor_PlayerController : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            PlayerController soundManager = (PlayerController)target;

            GUIStyle style = new GUIStyle (GUI.skin.textArea);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;

            GUIStyle style2 = new GUIStyle (GUI.skin.textArea);
            style2.alignment = TextAnchor.MiddleCenter;
            style2.fontStyle = FontStyle.Normal;

            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("===== Editor =====", style);
            EditorGUILayout.LabelField("=== Change State ===", style2);
            foreach (PlayerState state in Enum.GetValues(typeof(PlayerState))) {
                if (GUILayout.Button(state.ToString())) {
                    soundManager.ChangePlayerState(state);
                }
            }
        }
    }
}
