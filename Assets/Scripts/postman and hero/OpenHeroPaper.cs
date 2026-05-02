//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenHeroPaper : MonoBehaviour,IPointerClickHandler
{

    [Header("dialogue settings and others")]
    public DialogueManager dialogueManager;
    public bool IsHero=true;
    public bool IsPostman=false;

     public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if(dialogueManager!=null && dialogueManager.gameObject.activeInHierarchy)
            {
                dialogueManager.NextSentences();
                Debug.Log("tek tıklandı sonraki metine geçildi");
            }
        }
        
    }
}
