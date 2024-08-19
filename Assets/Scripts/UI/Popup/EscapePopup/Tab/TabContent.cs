using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BW
{
    public class TabContent : MonoBehaviour
    {
        private TabGroup tabGroup;

        private void Awake()
        {
            tabGroup = GetComponentInParent<TabGroup>();
        }
    }
}
