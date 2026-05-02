using UnityEngine;
using UnityEngine.EventSystems; //sürüklemek için gereli
using System.Collections;
//using Unity.VisualScripting; //düşme animasyonu için gerekli

public class DraggablePaper : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    public QuestData myQuestData;

    [Header("Fall Settings")]
    public float deskSurfaceY=60f; 
    public float fallSpeed=1000f;

    private Coroutine fallCoroutine; 

    [HideInInspector] public bool isReturned = false; // iade edildi mi


    [Header("Quest Completed")]
    public bool isNew=true; //ilk spawn mı
    public bool isCompleted = false; // Görev yapıldı mı?
    public int earnedGold = 0; // Bu kağıttan kazanılan net para
    public GameObject successStampObj;
    public GameObject failStampObj;

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
        //masaya düşerken tekrar havada yakala
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
        }

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
        
        canvasGroup.blocksRaycasts=true;
        transform.SetParent(parentAfterDrag);

        RectTransform rect=GetComponent<RectTransform>();
        //havadaysa düşür
        if (rect.localPosition.y > deskSurfaceY)
        {
            fallCoroutine=StartCoroutine(FallToDesk(rect));
        }
    }

    //düşme için
    private IEnumerator FallToDesk(RectTransform rect)
    {   //masaya değene kadar çalış
        while(rect.localPosition.y> deskSurfaceY)
        {
            rect.localPosition -= new Vector3(0,fallSpeed*Time.deltaTime,0);
            if (rect.localPosition.y <= deskSurfaceY)
            {
                rect.localPosition=new Vector3(rect.localPosition.x , deskSurfaceY , rect.localPosition.z);
                break;
            }
            yield return null; // sonraki framei bekle
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("quest: "+ myQuestData.questName+" \n for the "+myQuestData.preferredClass +" and "+ myQuestData.requiredStat+":"+myQuestData.statThreshold+" gold: "+myQuestData.rewardGold);
            UIManager.Instance.ShowQuestDetails(myQuestData);
        }
    }

    private void OnDestroy()
    {
        if(UIManager.Instance!=null) 
        {
            if(UIManager.Instance.currentQuestData==myQuestData)//curentla aynıysa questdetailsı da kapat 
            {
                UIManager.Instance.CloseQuestDetails();
            }
        }
    }

    public void MarkAsCompleted(int profit)
    {
        isCompleted=true;
        earnedGold=profit;
        if(successStampObj!=null) successStampObj.SetActive(true);
        if(failStampObj!=null) failStampObj.SetActive(false);
    }
    public void MarkAsFailed()
    {
        isCompleted=false;
        earnedGold=0;
        if(failStampObj!=null) failStampObj.SetActive(true);
    }
}
