using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BW
{
    public class StartTest : MonoBehaviour
    {
        public Button startButton;
        public string playScene = "Main";

        private void Awake()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() => SceneLoadManager.SceneLoad(playScene));
        }
    }
}