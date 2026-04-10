using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NegotiationManager : MonoBehaviour
{
   public static NegotiationManager Instance;

   [Header("UI refereance")]
   public GameObject negotiationPanel;
   public Slider offerSlider;
   public TextMeshProUGUI heroDialogueText;
    public TextMeshProUGUI offerValueText;
    public Button agreeButton;
    public Button rollDiceButton;
    public Button closeButton;

    [Header("current quest data")]
    private HeroStats currentHero;
    private QuestData currentQuest;
    private DraggablePaper currentPaper;
    private int relevantStatValue; //statın kahramandaki karşılığı
    private int hiddenChance;
    public int shopReputation = 50;

    [Header("Daktilo Ayarları (Kahraman)")]
    public float typeSpeed = 0.03f; // Harf hızı
    private Coroutine typingCoroutine; // Çalışan daktiloyu hafızada tutar

    

    void Awake()
    {
        if(Instance==null)Instance=this;
        else Destroy(gameObject);
    }

    void Start()
    {
        offerSlider.onValueChanged.AddListener(delegate{UpdateDialogue();});
        agreeButton.onClick.AddListener(AttemptAgreement);
        rollDiceButton.onClick.AddListener(RollDice);
        closeButton.onClick.AddListener(ClosePanel);
    }

    

    public void StartNegotiation(HeroStats hero, QuestData quest, DraggablePaper paper)
    {
        currentHero=hero;
        currentQuest=quest;
        currentPaper=paper;

        if(currentQuest==null) return;

        relevantStatValue=GetRelevantStat(currentHero,currentQuest.requiredStat);

        offerSlider.interactable=true;
        offerSlider.value=50;

        rollDiceButton.gameObject.SetActive(false);
        agreeButton.gameObject.SetActive(true);
        negotiationPanel.SetActive(true);

        heroDialogueText.transform.parent.gameObject.SetActive(true);
        UpdateDialogue();
        
    }

    private int GetRelevantStat(HeroStats hero, StatType reqStat)
    {
        switch (reqStat)
        {
            case StatType.Strength: return hero.strength;
            case StatType.Dexterity: return hero.dexterity;
            case StatType.Intelligence: return hero.intelligence;
            case StatType.Charisma: return hero.charisma;
            default: return 0;
        }
    }

    public void UpdateDialogue()
    {
        if (currentQuest == null || heroDialogueText == null || offerValueText == null) return;

        int offerPercentage = (int)offerSlider.value;
        //teklif edilen parayı yaz
        int offeredGold = (currentQuest.rewardGold * offerPercentage) / 100;
        int myProfit = currentQuest.rewardGold - offeredGold;

        offerValueText.text = $"Teklifin: %{offerPercentage} ({offeredGold} Altın) Karın: {myProfit} Altın";
        //zar beklentisi
        int expectedPower = relevantStatValue + 10; 
        int statDifference = expectedPower - currentQuest.statThreshold;

        hiddenChance = 40 + (offerPercentage - 50) + (shopReputation / 2) + (statDifference * 3);
        //şans +40la başlar + para %50 üstüyse şans artar az verirsen şans eksilir+ dükkan itiibarı ekstra buff verir+stat farkı şansı etkiler -+ yönde
        hiddenChance = Mathf.Clamp(hiddenChance, 5, 95); //şans 5 - 95 arasına fixler
        //class uyum bonusu
        bool isPreferedClass=(currentHero.heroClass==currentQuest.preferredClass);
        if(isPreferedClass) hiddenChance+=10;
        // Dialogr kısmı

            string reactionLine=""; //söylenecek cümle burda tutulur

            if(statDifference < -5 && offerPercentage < 60)  reactionLine = "Bu bir intihar görevi! Eğer ölmemi istiyorsan kesenin ağzını açmalısın!";
            else if (hiddenChance < 20) reactionLine = "Benimle dalga mı geçiyorsun? Bu paraya kılıcımı bile çekmem!";
            else if (hiddenChance < 40) reactionLine = "Komik bir teklif patron. Zırh bakımım bile bundan fazla tutar.";
            else if (hiddenChance < 60) reactionLine = "Hmm... Fena değil ama riskli. Biraz daha düşünmem lazım.";
            else if (hiddenChance < 80) reactionLine = "Makul bir teklif. Anlaştık, o işin icabına bakacağım.";
            else reactionLine = "Harika bir fiyat! Tüm yeteneklerim senin emrindedir!";
        
            ShowHeroText(reactionLine);
    }

    public void AttemptAgreement()
    {
        int roll=Random.Range(1,101);

        if (roll <= hiddenChance)
        {
            ShowHeroText("<color=green>Anlaşma sağlandı görev kabul edildi");
            agreeButton.gameObject.SetActive(false);
            rollDiceButton.gameObject.SetActive(true);
            offerSlider.interactable=false;
        }
        else
        {
            ShowHeroText("<color=red>masadan kalktı dükkandan çıkıyor");
            agreeButton.gameObject.SetActive(false);
            offerSlider.interactable = false;

            ReturnPaperToDesk();
        }
    }

    public void RollDice()
    {
        int d20Roll = Random.Range(1, 21);
        int totalScore = d20Roll + relevantStatValue;

        // Karımızı hesaplıyoruz (Toplam Ödül - Kahramana Verilen Pay)
        int offerPercentage = (int)offerSlider.value;
        int offeredGold = (currentQuest.rewardGold * offerPercentage) / 100;
        int myProfit = currentQuest.rewardGold - offeredGold;

        if (d20Roll == 20 || totalScore >= currentQuest.statThreshold)
        {
            ShowHeroText($"<color=green>GÖREV BAŞARILI!</color>");
            
            //Başarılı mührünü bas!
            if (currentPaper != null) 
            {
                currentPaper.MarkAsCompleted(myProfit);
                ReturnPaperToDesk(); // Mühürlü kağıt masaya düşsün
            }
            
        }
        else
        {
            int damage = currentQuest.statThreshold - totalScore;
            ShowHeroText($"<color=red>GÖREV BAŞARISIZ!</color>");
            
            // YENİ EKLEME: Görev başarısız oldu. 
            if (currentPaper != null)
            {
                currentPaper.MarkAsFailed();
                ReturnPaperToDesk(); 
            }
        }

        rollDiceButton.gameObject.SetActive(false);
    }

    private void ReturnPaperToDesk()
    {
        
        if (currentPaper != null)
        { 
            currentPaper.isReturned = true; 

            currentPaper.transform.SetParent(currentPaper.parentAfterDrag);

            CanvasGroup canvasGroup = currentPaper.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts=true;
            }
            currentPaper = null; 
        }
    }

    public void ClosePanel()
    {
        ReturnPaperToDesk();
        negotiationPanel.SetActive(false);
        
    }

   
    public void ShowHeroText(string textToType)
    {
        
        heroDialogueText.gameObject.SetActive(true); 
        
        // Eğer önceden yazılan bir yazı varsa durdur
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        
        // Yeni yazıyı başlat
        typingCoroutine = StartCoroutine(TypeText(textToType));
    }

    private System.Collections.IEnumerator TypeText(string textToType)
    {
        heroDialogueText.text = "";
        string visibleText = "";

        foreach (char letter in textToType.ToCharArray())
        {
            visibleText += letter;
            heroDialogueText.text = visibleText;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

}
