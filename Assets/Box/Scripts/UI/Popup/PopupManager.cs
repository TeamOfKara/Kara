using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    public class PopupManager : MonoSingleton<PopupManager>
    {
        [SerializeField] private Canvas canvas;
        public FadePopup fadePopup;

        public override void Awake()
        {
            base.Awake();

            GetResources();
        }

        private void GetResources()
        {
            Transform[] childs = this.GetComponentsInChildren<Transform>();
            foreach(var child in childs) {
                switch(child.name) {
                    case "Canvas":
                        child.TryGetComponent<Canvas>(out canvas);
                        break;
                    case "FadePopup":
                        child.TryGetComponent<FadePopup>(out fadePopup);
                        break;
                }
            }
        }
    }
}