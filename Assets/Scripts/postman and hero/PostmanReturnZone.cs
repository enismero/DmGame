using UnityEngine;
using UnityEngine.EventSystems;

public class PostmanReturnZone : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggablePaper paper = eventData.pointerDrag.GetComponent<DraggablePaper>();
        if (paper != null)
        {
            paper.isReturned = true;
            Debug.Log(paper.myQuestData.questName + " silindi");
            Destroy(paper.gameObject);
        }
    }
}
