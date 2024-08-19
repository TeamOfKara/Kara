using UnityEditor;
using UnityEditor.SceneManagement;

namespace BW
{
    public class Editor_PlayScene : EditorWindow
    {
        [MenuItem("Editor/PlayScene/1. Start")]
        static void Start() => ScenePlay(0);

        [MenuItem("Editor/PlayScene/2. Loading")]
        static void Loading() => ScenePlay(1);

        [MenuItem("Editor/PlayScene/3. Main")]
        static void Main() => ScenePlay(2);

        public static void ScenePlay(int SceneIndex)
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[SceneIndex].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);

            if (sceneAsset != null) {
                EditorSceneManager.playModeStartScene = sceneAsset;
                EditorApplication.EnterPlaymode();
                EditorApplication.quitting += Reset;
            }
        }

        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode(EnterPlayModeOptions option)
        {
            EditorApplication.update += Reset;
        }

        private static void Reset()
        {
            EditorSceneManager.playModeStartScene = null;
            EditorApplication.update -= Reset;
        }
    }
}