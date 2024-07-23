using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BW
{
    [Serializable]
    public struct TabColor
    {
        public Color idle;
        public Color hover;
        public Color active;
    }

    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private TabColor tabColor;
        [SerializeField] private List<TabButton> tabButtons = new List<TabButton>();        
        [SerializeField] private List<TabContent> tabContents = new List<TabContent>();
        [SerializeField, ReadOnly] private TabButton selectedTab;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; ++i) {
                switch (transform.GetChild(i).name) {
                    case "Tabs":
                        Subscribe_Tab(transform.GetChild(i));
                        break;
                    case "Contents":
                        Subscribe_Content(transform.GetChild(i));
                        break;
                }
            }
        }

        public void Subscribe_Tab(Transform target)
        {
            for (int i = 0; i < target.transform.childCount; ++i) {
                TabButton button = target.transform.GetChild(i).GetComponent<TabButton>();
                tabButtons.Add(button);
            }
        }

        public void Subscribe_Content(Transform target)
        {
            for (int i = 0; i < target.transform.childCount; ++i) {
                TabContent content = target.transform.GetChild(i).GetComponent<TabContent>();
                tabContents.Add(content);
            }
        }

        private void OnEnable()
        {
            foreach (var button in tabButtons) {
                int index = button.transform.GetSiblingIndex();
                if (index == 0) {
                    selectedTab = button;
                    button.GetComponent<Image>().color = tabColor.active;
                }
                else {
                    button.GetComponent<Image>().color = tabColor.idle;
                }
            }

            foreach (var content in tabContents) {
                int index = content.transform.GetSiblingIndex();
                if (index == 0) {
                    content.gameObject.SetActive(true);
                }
                else {
                    content.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Pointer Enter
        /// </summary>
        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab) {
                button.image.color = tabColor.hover;
            }
        }

        /// <summary>
        /// Pointer Exit
        /// </summary>
        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        /// <summary>
        /// Pointer Click
        /// </summary>
        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null) {
                selectedTab.Deselect();
            }
            selectedTab = button;
            selectedTab.Select();
            
            ResetTabs();
            button.image.color = tabColor.active;

            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < tabContents.Count; ++i) {
                if (index == tabContents[i].transform.GetSiblingIndex()) {
                    tabContents[i].gameObject.SetActive(true);
                }
                else {
                    tabContents[i].gameObject.SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons) {
                if (selectedTab != null && button == selectedTab) { continue; }
                button.image.color = tabColor.idle;
            }
        }

        /// <summary>
        /// Pointer Selected Event
        /// </summary>
        public void Select(TabButton button)
        {

        }

        /// <summary>
        /// Pointer UnSelected Event
        /// </summary>
        public void Deselect(TabButton button)
        {

        }
    }
}