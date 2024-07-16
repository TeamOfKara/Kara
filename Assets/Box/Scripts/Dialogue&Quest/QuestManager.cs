using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BW
{
    // 퀘스트 데이터 (ID, Title, 퀘스트 내용, 보상)
    [System.Serializable] 
    public struct Reward
    {
        public string reward;

        public Reward(string reward)
        {
            this.reward = reward;
        }
    }

    // 퀘스트 데이터 (ID, Title, 퀘스트 내용, 보상)
    [System.Serializable] 
    public struct QuestData
    {
        public int id;
        public string title;
        public string charactor;
        public string quest;
        public List<Reward> reward;

        public QuestData(int id, string title, string charactor, string quest, List<Reward> reward)
        {
            this.id = id;
            this.title = title;
            this.charactor = charactor;
            this.quest = quest;
            this.reward = reward;
        }
    }

    public class QuestManager : MonoSingleton<QuestManager>
    {
        [SerializeField, Tooltip("Assets/Box/Resources/CSV/"), ReadOnly] private string csvPath = "CSV/";
        [SerializeField] private List<QuestData> questList = new List<QuestData>();

        public void GetQuestData(string csvName)
        {
            List<Dictionary<string, object>> data = CSVReader.Read(csvPath + csvName);

            List<Reward> rewardList = new List<Reward>();

            for (int i = 0; i < data.Count; ++i) {
                // Get Data
                int id = (int)data[i]["ID"];
                string title = data[i]["Title"].ToString();
                string charactor = data[i]["Charactor"].ToString();
                string quest = data[i]["Quest"].ToString();
                string reward = data[i]["Reward"].ToString();
                //Debug.Log(id + ",  " + title + ",  " + charactor + ",  " + quest + ",  " + reward);

                // Set Reward
                string[] rewards  = reward.Split("/");
                foreach (string re in rewards) {
                    rewardList.Add(new Reward(re));
                }

                // Set Data
                this.questList.Add(new QuestData(id, title, charactor, quest, rewardList)); // 마지막 대사 추가

                // Reset
                rewardList = new List<Reward>();
            }
        }
    }
}
