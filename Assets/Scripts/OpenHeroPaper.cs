using UnityEngine;
using UnityEngine.EventSystems;

public class OpenHeroPaper : MonoBehaviour,IPointerClickHandler
{
     public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            
            UIManager.Instance.OpenHeroDetails();
        }
    }
}
