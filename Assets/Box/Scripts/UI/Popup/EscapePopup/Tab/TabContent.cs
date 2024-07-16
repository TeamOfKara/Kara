using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BW
{
    public class TabContent : MonoBehaviour
    {
        private TabGroup tabGroup;
        public int index;

        private void Awake()
        {
            tabGroup = GetComponentInParent<TabGroup>();
            tabGroup.Subscribe(this);

            index = this.transform.GetSiblingIndex();

            if (index == 0) {
                this.gameObject.SetActive(true);
            }
            else {
                this.gameObject.SetActive(false);
            }
        }
    }
}
