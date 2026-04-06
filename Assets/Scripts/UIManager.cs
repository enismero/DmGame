using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Hero UI referance")]
        public GameObject heroDetailPanel;
        public TextMeshProUGUI classNameText;
        public TextMeshProUGUI statsText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI goldText;

    [Header("Quest detail referance")]
        public GameObject questDetailPanel;
        public TextMeshProUGUI questNameText;
        public TextMeshProUGUI requiredStatText;
        public TextMeshProUGUI preferredClassText;
        
        public TextMeshProUGUI rewardText;
        public TextMeshProUGUI daysText;


        void Awake()
    {
        if(Instance==null) Instance=this;
        else Destroy(gameObject);
    }


    public void UpdateHeroUI(HeroStats hero)
    {
        heroDetailPanel.SetActive(false);
        classNameText.text="Class: "+hero.heroClass.ToString();
        statsText.text=$"Str: {hero.strength}\nDex: {hero.dexterity}\nInt: {hero.intelligence}\nCha: {hero.charisma}";
        healthText.text=$"Hp: {hero.maxHealth}";
        goldText.text=$"Gold: {hero.goldAmount}";
    }
    public void CloseHeroDetails()
    {
        heroDetailPanel.SetActive(false);
    }
    public void OpenHeroDetails()
    {
        heroDetailPanel.SetActive(true);
    }


    public void ShowQuestDetails(QuestData data)
    {
        questDetailPanel.SetActive(true);

        questNameText.text=data.questName;
        requiredStatText.text= data.requiredStat.ToString()+": "+data.statThreshold.ToString();
        preferredClassText.text=data.preferredClass.ToString();
        rewardText.text="Gold: "+data.rewardGold.ToString();
        daysText.text=data.daysRemaining.ToString()+" Days";

    }
    public void CloseQuestDetails()
    {
        questDetailPanel.SetActive(false);
    }
}
