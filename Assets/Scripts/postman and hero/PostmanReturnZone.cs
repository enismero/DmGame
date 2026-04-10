using UnityEngine;
using UnityEngine.EventSystems;

public class PostmanReturnZone : MonoBehaviour,IDropHandler
{
    public OpenHeroPaper openHeroPaper;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();

        if (paper != null&& openHeroPaper.IsPostman)
        {
            if (paper.isNew)
            {
                Debug.Log("İade Kabul Edildi");
                Destroy(paper.gameObject); 
                return;
            }

            if (paper.isCompleted)
            {
                Debug.Log(paper.myQuestData.questName + "  yapıldı postacıya teslim edildi " +paper.earnedGold+"altın kazandın");
                Destroy(paper.gameObject);
                return;
            }
            
            Debug.Log("Postacı: 'Üzgünüm, bu işi kabul etmiştin. ");
            paper.isReturned = true;
            
        }

        DraggableHeroDetails heroCard = eventData.pointerDrag.GetComponent<DraggableHeroDetails>();
        if (heroCard != null && openHeroPaper.IsHero)
        {
            Debug.Log("Kahraman reddedildi");
            
            // Masadaki kimliği yok et
            Destroy(heroCard.gameObject);

            
            WalkTo heroWalk = Object.FindFirstObjectByType<WalkTo>();

            if (heroWalk != null)
            {
                heroWalk.GoBack();
            }
        }
    }
}
