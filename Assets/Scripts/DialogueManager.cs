using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
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
    
    

    void OnEnable()
    {
        currentLineIndex = 0;
        StartNextLine();
    }


    public void NextSentences()
    {
        if (isTyping)
        {   //yazarken tıklarsa durdur ve fulle
            StopCoroutine(typingCorountine);
            dialogueText.text=messages[currentLineIndex];
            isTyping=false;
            return;
        }
        
        
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
            gameObject.SetActive(false);
            Debug.Log("diyalog bitti");
        }
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
