using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //farede dragable obje varsa al
        if (eventData.pointerDrag != null)
        {
            //dragable script var mı
            DraggablePaper paper= eventData.pointerDrag.GetComponent<DraggablePaper>();
            if (paper != null)
            {
                //paperın parentenı dropzone ayarla
                paper.parentAfterDrag=transform;
                Debug.Log("paper is drop");
            }
        }
    }
}

