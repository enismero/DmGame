using UnityEngine;
using UnityEngine.EventSystems; //sürüklemek için gereli

public class DraggablePaper : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;

    public QuestData myQuestData;

    void Awake()
    {
        canvasGroup= GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup=gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //sürüklemeye başlamadanki yeri kaydet
        parentAfterDrag=transform.parent; 
        //sürüklerkken en üst canvasa taşı
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //fareyle sürüklerken dropzonu algılamak için raycasti kapatıyoruz
        canvasGroup.blocksRaycasts=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //sürükleme bittiğinde yeni yere koy bırakılmadıysa eski yerine
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts=true;

    }
}
