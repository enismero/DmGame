using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
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

    

    void OnEnable()
    {
        currentLineIndex = 0;
        StartNextLine();
    }
    public void NextSentences()
    {
        if (isTyping)
        {
            StopCoroutine(typingCorountine);
            string fullSentence=messages[currentLineIndex];
            isTyping=false;
        }
        else
        {
            currentLineIndex++;
            StartNextLine();
        }   
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

           // if (visibleText.Length > maxCharacters) visibleText=visibleText.Substring(1);
            
            dialogueText.text=visibleText;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping=false;
    }
}
