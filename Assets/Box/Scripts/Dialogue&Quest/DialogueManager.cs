using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW
{
    // 대사 내용 (캐릭터, 대사)
    [System.Serializable]
    public struct Dialogue
    {
        public string charactor;
        public string dialogue;

        public Dialogue(string charactor, string dialogue)
        {
            this.charactor = charactor;
            this.dialogue = dialogue;
        }
    }

    // 대사 데이터 (ID, Title, 대사들)
    [System.Serializable] 
    public struct DialogueData
    {
        public int id;
        public string title;
        public List<Dialogue> dialogueData;

        public DialogueData(int id, string title, List<Dialogue> dialogueData)
        {
            this.id = id;
            this.title = title;
            this.dialogueData = dialogueData;
        }
    }

    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [SerializeField, Tooltip("Assets/Box/Resources/CSV/"), ReadOnly] private string csvPath = "CSV/";
        [SerializeField] private List<DialogueData> dialogueList = new List<DialogueData>();

        public void GetDialogueData(string csvName)
        {
            List<Dictionary<string, object>> data = CSVReader.Read(csvPath + csvName);

            int id = -1;
            string title = "";
            string charactor = "";
            List<Dialogue> dialogue = new List<Dialogue>();

            for (int i = 0; i < data.Count; ++i) {
                // Get Data
                int cur_id = data[i]["ID"].ToString() == "" ? id : (int)data[i]["ID"];
                string cur_title = data[i]["Title"].ToString() == "" ? title : data[i]["Title"].ToString();
                string cur_charactor = data[i]["Charactor"].ToString() == "" ? charactor : data[i]["Charactor"].ToString();
                string talk = data[i]["Dialogue"].ToString();
                //Debug.Log(cur_id + ",  " + cur_title + ",  " + cur_charactor + ",  " + talk);

                // Set Data
                if (id != cur_id) {
                    if (id != -1) this.dialogueList.Add(new DialogueData(id, title, dialogue)); // 새로운 대사 추가

                    // Reset
                    id = cur_id;
                    title = cur_title;
                    dialogue = new List<Dialogue>();
                }
                // Temp Add
                charactor = cur_charactor == "" ? charactor : cur_charactor;
                dialogue.Add(new Dialogue(charactor, talk));
            }
            // Set Data
            this.dialogueList.Add(new DialogueData(id, title, dialogue)); // 마지막 대사 추가
        }
    }
}