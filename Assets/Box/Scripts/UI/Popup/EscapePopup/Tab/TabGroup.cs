using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private List<TabButton> tabButtons = new List<TabButton>();
        [SerializeField] private Color tabIdle = new Color32(255, 255, 255, 128);
        [SerializeField] private Color tabHover = new Color32(255, 255, 255, 255);
        [SerializeField] private Color tabActive = new Color32(255, 255, 128, 255);
        [SerializeField] private TabButton selectedTab;

        [SerializeField] private List<TabContent> tabContents = new List<TabContent>();

        public void Subscribe(TabButton button)
        {
            tabButtons.Add(button);
            button.image.color = tabIdle;
        }

        public void Subscribe(TabContent content)
        {
            tabContents.Add(content);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab) {
                button.image.color = tabHover;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null) {
                selectedTab.Deselect();
            }
            selectedTab = button;
            selectedTab.Select();
            
            ResetTabs();
            button.image.color = tabActive;

            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < tabContents.Count; ++i) {
                if (index == tabContents[i].index) {
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
                button.image.color = tabIdle;
            }
        }

        public void Select(TabButton button)
        {

        }

        public void Deselect(TabButton button)
        {

        }
    }
}
