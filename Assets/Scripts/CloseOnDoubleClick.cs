using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnDoubleClick : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            UIManager.Instance.CloseQuestDetails();
        }
    }
}
