using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [Header("referanges")]
        public TaskManager taskManager;
        public int SpawnQuestAmount=8;
        public GameManager gameManager;

    [Header("Dialogue Settings")]
        public TextMeshProUGUI dialogueText;
        public float typingSpeed=0.05f;
        public int maxCharacters= 50;

    [Header("mesages")]
    [TextArea(3,10)]
    public string[] messages;


    private int currentLineIndex=0;
    private bool isTyping=false;
    private Coroutine typingCorountine;
    public OpenHeroPaper openHeroPaper;
    public WalkTo walkTo;
    

    private bool isDynamicMode=false;
    private string currentDynamicMessage="";
    private bool hasIntroduced=false;

    void Awake()
    {
        if(Instance==null) Instance=this;
    }

    void OnEnable()
    {
        if (isDynamicMode||hasIntroduced) return;

        currentLineIndex = 0;
        StartNextLine();
    }
    void Oisable()
    {
        isDynamicMode=false;        
    }

    public void NextSentences()
    {
        if (isTyping)
        {   //yazarken tıklarsa durdur ve fulle
            StopCoroutine(typingCorountine);
            //pazarlık modunda dinamiği değilse listedekini başlat
            dialogueText.text= isDynamicMode?currentDynamicMessage:messages[currentLineIndex];
            isTyping=false;
            return;
        }
        if (isDynamicMode) return;
        
            currentLineIndex++;
        //postman için ilk gün toutorial kısmı quest papaer spawnlama gir ve çık
            if (openHeroPaper!=null)
                {
                    if(openHeroPaper.IsPostman && taskManager != null)
                    {
                        if (currentLineIndex == 1)
                        {
                        for(int i = 0; i <= SpawnQuestAmount-1; i++)
                        {
                            taskManager.SpawnQuestOnDesk();
                        
                        }
                        Debug.Log("Quest papers was spawned("+(SpawnQuestAmount-1) + ")");
                        }
                    }
                    //kimlik bırakma
                    else if (openHeroPaper.IsHero && gameManager != null)
                    {
                        if (currentLineIndex == 1)
                        {
                        gameManager.SpawnHeroCardOnDesk();
                        }
                    }
                }

            if (currentLineIndex >= messages.Length)
                    {
                        if (walkTo != null&&openHeroPaper.IsPostman)
                        {
                            walkTo.GoBack();
                            gameObject.SetActive(false);
                            
                            return;
                        }
                    }
        
            StartNextLine();
        
    }


    public void StartNextLine()
    {
        if(currentLineIndex<messages.Length) typingCorountine=StartCoroutine(TypeText(messages[currentLineIndex]));
        else
        {
            hasIntroduced=true;
            gameObject.SetActive(false);
            Debug.Log("diyalog bitti");
        }
    }

    public void ShowNegotiationLine(string textToType)
    {
        isDynamicMode=true;
        currentDynamicMessage = textToType; // Yazıyı hafızaya al 

        gameObject.SetActive(true); // Paneli aç
        
        if (typingCorountine != null) StopCoroutine(typingCorountine);
        typingCorountine = StartCoroutine(TypeText(currentDynamicMessage));
    }



    private IEnumerator TypeText(string textToType)
    {
        isTyping=true;
        dialogueText.text="";
        string visibleText="";

        foreach(char letter in textToType.ToCharArray())
        {
            visibleText+=letter;

            dialogueText.text=visibleText;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping=false;
    }
}
