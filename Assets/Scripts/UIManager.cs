using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    

    [HideInInspector] public QuestData currentQuestData;

    [Header("Hero UI referance")]
        public GameObject heroDetailPanel;
        public TextMeshProUGUI classNameText;
        public TextMeshProUGUI statsText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI heroNameText;
        
        public HeroGenerator doorHero;

    [Header("Quest detail referance")]
        public GameObject questDetailPanel;
        public TextMeshProUGUI questNameText;
        public TextMeshProUGUI requiredStatText;
        public TextMeshProUGUI preferredClassText;
        public TextMeshProUGUI rewardText;
        public TextMeshProUGUI daysText;

        

    [Header("Hero portrait settings")]
        public Image detailBody;
        public Image detailHead;
        public Image detailPupil;
        public Image detailEyeShape;
        public Image detailNose;
        public Image detailMouth;
        public Image detailBeard;
        public Image detailMustache;
        public Image detailHair;
        public Image detailDecor;


        void Awake()
    {
        if(Instance==null) Instance=this;
        else Destroy(gameObject);
    }


    public void UpdateHeroUI(HeroStats hero)
    {
        heroDetailPanel.SetActive(false);
        
        heroNameText.text =doorHero.currentHeroName;
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
        
        UpdateHeroPhotoInDetailPanel();
        heroDetailPanel.SetActive(true);
    }



    public void ShowQuestDetails(QuestData data)
    {
        currentQuestData=data;//aklında tut hangisinin açıldığını
        questDetailPanel.SetActive(true);

        questNameText.text=data.questName;
        requiredStatText.text= data.requiredStat.ToString()+": "+data.statThreshold.ToString();
        preferredClassText.text=data.preferredClass.ToString();
        rewardText.text="Gold: "+data.rewardGold.ToString();
        daysText.text=data.daysRemaining.ToString()+" Days";

    }

    public void CloseQuestDetails()
    {
        currentQuestData=null;//verileri temizle
        questDetailPanel.SetActive(false);
    }


    public void UpdateHeroPhotoInDetailPanel()
    {
        if (doorHero == null) 
    {
        doorHero = GameObject.FindAnyObjectByType<HeroGenerator>();
    }

    if (doorHero != null)
    {
        // 2. ADIM: Kapıdaki katmanları, paneldeki 1, 1(1) gibi isimlendirdiğin objelere kopyala
        CopyLayer(doorHero.bodyLayer, detailBody);
        CopyLayer(doorHero.headLayer, detailHead);
        CopyLayer(doorHero.pupilLayer, detailPupil);
        CopyLayer(doorHero.eyeShapeLayer, detailEyeShape);
        CopyLayer(doorHero.noseLayer, detailNose);
        CopyLayer(doorHero.mouthLayer, detailMouth);
        CopyLayer(doorHero.beardLayer, detailBeard);
        CopyLayer(doorHero.mustacheLayer, detailMustache);
        CopyLayer(doorHero.hairLayer, detailHair);
        CopyLayer(doorHero.decorLayer, detailDecor);
        
        Debug.Log("Katmanlar başarıyla kopyalandı!");
    }
    else
    {
        Debug.LogError("HeroGenerator sahnede bulunamadı!");
    }

    }
    private void CopyLayer(Image source,Image target)
    {
        if(source==null ||target==null) return;
        target.sprite=source.sprite;
        target.color=source.color;
        target.enabled=(source.sprite!=null);
    }


    }

