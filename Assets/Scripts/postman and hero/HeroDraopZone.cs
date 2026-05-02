using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDraopZone : MonoBehaviour,IDropHandler
{
    private GameManager gameManager;
    private WalkTo walkTo;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();
        //atılan paper mı 
        if (paper == null || gameManager == null)
        {
            return;
        }
        
            if (paper.isCompleted)
            {
                Debug.Log("sadece postacıya verilebilir görev tamamlanmış");
                paper.isReturned=true;
                return;
            }
            
        
         
        
            //verileri kaydet
            QuestData quest = paper.myQuestData;
            HeroStats hero = gameManager.newHero;
            
            Debug.Log($"{hero.heroName}, '{quest.questName}' için anlaşma vakti ");

            paper.isReturned = false;
            NegotiationManager.Instance.StartNegotiation(hero, quest, paper);
    }

}
