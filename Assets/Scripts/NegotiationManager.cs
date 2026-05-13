using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NegotiationManager : MonoBehaviour
{
   public static NegotiationManager Instance;

   [Header("UI refereance")]
   public TextMeshProUGUI heroDialogueText;
   public GameObject dialogueContainer;
    
  
     [Header("Sabır barı")]
     public Slider patienceBar; //sabır barı
     public float maxPatience=100f;
     public float currentPatience; 
     public float rejection=25f; //düşecek sabır
    

    [Header("current quest data")]
    private HeroStats currentHero;
    private QuestData currentQuest;
    private DraggablePaper currentPaper;
   

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
        currentPatience = maxPatience;
        UpdatePatienceUI();;
    }

    

    public void StartNegotiation(HeroStats hero, QuestData quest, DraggablePaper paper)
    {
        currentHero=hero;
        currentQuest=quest;
        currentPaper=paper;

        // Yeni kahraman geldiğinde sabrını tekrar doldur
        currentPatience = maxPatience;
        UpdatePatienceUI();

        ShowHeroText($"{hero.heroName}: How much money will I get for this job?");
        
    }


    public void OnPouchReceived(MoneyPouch pouch)
    {
        if (currentQuest == null) return;

        int expectedGold = currentQuest.rewardGold / 2; //kabul edeceği tutar ödülün yarısı olarak ayarlı
        int offeredGold = pouch.totalGoldInPouch;

        //kabul
        if (offeredGold >= expectedGold)
        {
            Debug.Log("Karakter kabul etti.");
            ShowHeroText("This is good price ,Agreed");

            if (currentPaper != null) 
            {
                
                int myProfit = currentQuest.rewardGold - offeredGold; // Dükkanın karı
                currentPaper.MarkAsCompleted(myProfit); // Kağıda verildi
                ReturnPaperToDesk(); // Kağıdı masaya geri gönder
            }
            pouch.ResetPouch(); // Keseyi sıfırla ve masaya geri gönder
            
        }
        //redd
        else
        {
            Debug.Log("Karakter reddetti.");
            currentPatience -= rejection; // Sabrını düşür
            UpdatePatienceUI();
            pouch.ResetPouch(); // Reddedince keseyi sıfırla
            //sabrı biterse
            if (currentPatience <= 0)
            {
                ShowHeroText("Are you kidding me? ");
                if (currentPaper != null)
                {
                    ReturnPaperToDesk();
                }
                Debug.Log("Karakter reddetti gidiyo.");
            }
            //sabrı varsa
            else
            {
                ShowHeroText("Give some more money...");
                Debug.Log("Karakter reddetti. teklif bekliyor");
            }
        }
    }

    private void UpdatePatienceUI()
    {
        if(patienceBar!=null)
        {
            patienceBar.value=currentPatience/maxPatience;
        }
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

    private void ClearNegotiation()
        {
            currentQuest = null;
            currentHero = default;
        }
   
    public void ShowHeroText(string textToType)
    {
        
        if (dialogueContainer != null) dialogueContainer.SetActive(true);
        if (heroDialogueText != null) heroDialogueText.gameObject.SetActive(true);

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
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
