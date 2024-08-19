using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BW
{
    public class SceneLoadManager : MonoBehaviour
    {
        static string sceneName = "Start";
        [SerializeField] private Slider progressbar;
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private TextMeshProUGUI progressText;

        public static void SceneLoad(string scene)
        {
            sceneName = scene;
            // Fade Out
            SceneManager.LoadScene("Loading");
        }

        private void Start()
        {
            StartCoroutine(SceneLoadCoroutine());
        }

        private IEnumerator SceneLoadCoroutine()
        {
            yield return null;
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            float timer = 0.0f;

            while (!op.isDone) {
                yield return null;
                timer += Time.deltaTime;

                if (op.progress < 0.9f) {
                    progressbar.value = Mathf.Lerp(progressbar.value, op.progress, timer);
                    progressText.text = (progressbar.value * 100f).ToString("F1") + " %";
                    if (progressbar.value >= op.progress) {
                        timer = 0f;
                    }
                }
                else {
                    progressbar.value = Mathf.Lerp(progressbar.value, 1f, timer);
                    progressText.text = "100 %";
                    if (progressbar.value == 1.0f) {
                        op.allowSceneActivation = true;
                        // Fade In
                        yield break;
                    }
                }
            }
            
        }
    }
}