using UnityEngine;
using UnityEngine.EventSystems;

public class CloseHeroPaper : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            
            UIManager.Instance.CloseHeroDetails();
        }
    }

}
