using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestData
{
    public string questName;
    public StatType requiredStat;
    public HeroClass preferredClass;
    public int statThreshold;
    public int rewardGold;
    public int daysRemaining;

}


public class TaskManager : MonoBehaviour
{
  [Header("prefab and area")]
  public GameObject questPaperPrefab;
  public Transform postmanDeskArea;

  [Header("Active quest data")]
  public List<QuestData> currentBoardQuest= new List<QuestData>();


    void Start() { SpawnQuestOnDesk(); }
    public void SpawnQuestOnDesk()
    {
        QuestData newData= GenerateRandomQuestData();
        GameObject newPaper =Instantiate(questPaperPrefab,postmanDeskArea);
        newPaper.transform.localPosition=Vector3.zero; //masanın ortasına sabitle

        DraggablePaper paperScript = newPaper.GetComponent<DraggablePaper>();
        if(paperScript != null)
        {
            paperScript.myQuestData=newData;
        }
    }


    private QuestData GenerateRandomQuestData()
    {
        QuestData q=new QuestData();
        q.requiredStat=(StatType)Random.Range(0,4);

        switch (q.requiredStat)
        {
            case StatType.Strength:
                string[] strQuests={"Help remove the rock on the cave","Defeat the Trolls","Escort the trader caravan","Break up the tavern brawl","Smash the Castle Gate"};
                q.questName=strQuests[Random.Range(0,strQuests.Length)];
                q.preferredClass=HeroClass.Knight; 
                break;

            case StatType.Dexterity: 
                string[] dexQuests={"Deliver the Letter","Steal at the Bazaar","Tail the Assassin","Infiltrate the Pirate Ship","Survive the Trapped Room"};
                q.questName=dexQuests[Random.Range(0,dexQuests.Length)];
                q.preferredClass=HeroClass.Rogue;
                break;

            case StatType.Intelligence:
                string[] intQuests={"Translate the Ancient Runes","Disrupt the Ritual","Conduct an Alchemical Experiment","Exorcise the Library Ghost","Decipher the Treasure Map"};
                q.questName=intQuests[Random.Range(0,intQuests.Length)];
                q.preferredClass=HeroClass.Wizard;
                break;

            case StatType.Charisma:
                string[] chaQuests={"Convince the Rebel Villagers","Perform at the Royal Wedding","Deal with the Guild Traders","Deceive the Cult Leader","Make Peace Between Gangs"};
                q.questName=chaQuests[Random.Range(0,chaQuests.Length)];
                q.preferredClass=HeroClass.Bard;
                break;
        }

        //difficulty and awards
        q.statThreshold=Random.Range(10,25);
        q.rewardGold=q.statThreshold*5;
        q.daysRemaining=Random.Range(1,5);

        return q;
    }


    public void AddQuestToLogicList(QuestData data)
    {
        if(!currentBoardQuest.Contains(data)) currentBoardQuest.Add(data);
    }

    public void CleanUpExpiredQuests()
    {
        for(int i= currentBoardQuest.Count-1; i>=0 ; i--)
        {
            currentBoardQuest[i].daysRemaining--;
            if (currentBoardQuest[i].daysRemaining <= 0)
            {
                currentBoardQuest.RemoveAt(i);
            }
        }
    }
}
