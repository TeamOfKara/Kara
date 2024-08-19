using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    public class PopupManager : MonoSingleton<PopupManager>
    {
        [SerializeField] private Canvas canvas;
        public FadePopup fadePopup;
        public EscapePopup escapePopup;
    }
}