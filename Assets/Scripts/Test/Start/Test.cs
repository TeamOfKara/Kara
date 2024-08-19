using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            // 페이드아웃 팝업
            //PopupManager.instance.fadePopup.FadeOut();

            // 세팅 팝업
            //PopupManager.instance.escapePopup.EscapePopupToggle();

            // 대사 가져오기
            DialogueManager.instance.GetDialogueData("KaraDataTest");

            // 퀘스트 가져오기
            //QuestManager.instance.GetQuestData("KaraQuestDataTest");
        }
    }
}