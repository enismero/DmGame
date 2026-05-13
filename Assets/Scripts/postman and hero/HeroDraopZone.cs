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
        //atılan kese mi
        MoneyPouch pouch=eventData.pointerDrag.GetComponent<MoneyPouch>();
        if (pouch != null)
        { 
            NegotiationManager.Instance.OnPouchReceived(pouch);
            return;
        } 


          //atılan paper mı 
        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();
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
            HeroStats hero = gameManager.newHero;
            if (string.IsNullOrEmpty(hero.heroName))
        {
            Debug.LogWarning("Masada kahraman yok, kağıt verilemez!");
            paper.isReturned = true; // Kahraman yoksa kağıdı geri yolla
            return; 
        }

            //verileri kaydet
            QuestData quest = paper.myQuestData;
            
            Debug.Log($"{hero.heroName}, '{quest.questName}' için anlaşma vakti ");

            paper.isReturned = false;
            NegotiationManager.Instance.StartNegotiation(hero, quest, paper);
    }

}
