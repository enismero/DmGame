using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDraopZone : MonoBehaviour,IDropHandler
{
    private GameManager gameManager;
    private WalkTo walkTo;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        walkTo = GetComponent<WalkTo>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();
        //atılan paper mı 
        if (paper != null && gameManager != null)
        {
            QuestData quest = paper.myQuestData;
            HeroStats hero = gameManager.newHero;
            //verileri kaydet

            
            Debug.Log($"{hero.heroName}, '{quest.questName}' görevini aldı ");

            paper.isReturned = true; 
            Destroy(paper.gameObject);

        }
    }
}
