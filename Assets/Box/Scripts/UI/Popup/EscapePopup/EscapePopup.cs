using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BW
{
    public class EscapePopup : MonoBehaviour
    {
        [SerializeField] private GameObject escapePanel;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            escapePanel.SetActive(false);
            quitButton.onClick.AddListener(() => EscapePopupToggle());
        }

        public void EscapePopupToggle()
        {
            if (!escapePanel.activeSelf) {
                escapePanel.SetActive(true);
            }
            else {
                escapePanel.SetActive(false);
            }
        }
    }
}
