using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BW
{
    [CustomEditor(typeof(SoundManager))]
    public class Editor_SoundManager : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            SoundManager soundManager = (SoundManager)target;

            GUIStyle style = new GUIStyle (GUI.skin.textArea);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;

            GUIStyle style2 = new GUIStyle (GUI.skin.textArea);
            style2.alignment = TextAnchor.MiddleCenter;
            style2.fontStyle = FontStyle.Normal;

            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("===== Editor =====", style);
            EditorGUILayout.LabelField("=== Music ===", style2);
            for (int i = 0; i < soundManager.misicClip_Test.Length; ++i) {
                if (GUILayout.Button("Music_" + i)) {
                    SoundManager.instance.PlayMusic(soundManager.misicClip_Test[i]);
                }
            }

            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("=== SFX ===", style2);
            for (int i = 0; i < soundManager.sfxClip_Test.Length; ++i) {
                if (GUILayout.Button("SFX_" + i)) {
                    SoundManager.instance.PlaySFX(soundManager.sfxClip_Test[i]);
                }
            }
        }
    }
}
