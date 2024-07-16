using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    public class EscapePopup : MonoBehaviour
    {
        [SerializeField] private GameObject escapePanel;

        private void Awake()
        {
            escapePanel.SetActive(false);
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
