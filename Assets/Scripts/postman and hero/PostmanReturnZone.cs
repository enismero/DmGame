using UnityEngine;
using UnityEngine.EventSystems;

public class PostmanReturnZone : MonoBehaviour,IDropHandler
{
    public OpenHeroPaper openHeroPaper;
    public void OnDrop(PointerEventData eventData)
    {
        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();
        if (paper != null)
        {
            paper.isReturned = true;
            Debug.Log(paper.myQuestData.questName + " silindi");
            Destroy(paper.gameObject);
            return;
        }

        DraggableHeroDetails heroCard = eventData.pointerDrag.GetComponent<DraggableHeroDetails>();
        if (heroCard != null)
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
