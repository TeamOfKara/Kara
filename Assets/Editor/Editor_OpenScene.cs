using UnityEditor;
using UnityEditor.SceneManagement;

namespace BW
{
    public class Editor_OpenScene : EditorWindow
    {
        [MenuItem("Editor/OpenScene/1. Start")]
        static void Start() => SceneOpen(0);

        [MenuItem("Editor/OpenScene/2. Loading")]
        static void Loading() => SceneOpen(1);

        [MenuItem("Editor/OpenScene/3. Main")]
        static void Main() => SceneOpen(2);

        static public void SceneOpen(int SceneIndex)
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[SceneIndex].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            var sceneName = sceneAsset.ToString().Split(' ');

            if (sceneAsset != null) {
                EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName[0] + ".unity");
            }
        }   
    }
}