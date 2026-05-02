using UnityEngine;
using UnityEngine.EventSystems; //sürüklemek için gereli
using System.Collections;

public class DraggableDice : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
   private CanvasGroup canvasGroup;
   [Header("Dice Animation Settings")]
    public Animator d20Animator;
    public float rollDuration = 1.5f; // Zarın dönme süresi
    private bool isRolling = false;// Zar dönerken sürüklenmesini veya tekrar tıklanmasını engellemek için

    [Header("fall settings")]
    public float deskSurfaceY=-100f;
    public float fallSpeed = 1000f;
    private Coroutine fallCoroutine;



    void Awake()
    {
       
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        if (d20Animator == null) 
        {
            d20Animator = GetComponent<Animator>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isRolling) return;
        if (fallCoroutine != null) StopCoroutine(fallCoroutine);
        transform.SetAsLastSibling();//sürüklerkken en üst canvasa taşı
        //fareyle sürüklerken dropzonu algılamak için raycasti kapatıyoruz
        canvasGroup.blocksRaycasts=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isRolling) return;
        transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //sürükleme bittiğinde yeni yere koy bırakılmadıysa eski yerine
        
        canvasGroup.blocksRaycasts=true;

        RectTransform rect=GetComponent<RectTransform>();
        //havadaysa düşür
        if (rect.localPosition.y > deskSurfaceY)
        {
            fallCoroutine=StartCoroutine(FallToDesk(rect));
        }
    }

    private IEnumerator FallToDesk(RectTransform rect)
    {
        while (rect.localPosition.y > deskSurfaceY)
        {
            rect.localPosition -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
            if (rect.localPosition.y <= deskSurfaceY)
            {
                rect.localPosition = new Vector3(rect.localPosition.x, deskSurfaceY, rect.localPosition.z);
                break;
            }
            yield return null; 
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Eğer zar dönmüyorsa VE çift tıklandıysa zarı at
        if (!isRolling && eventData.clickCount == 2)
        {
            StartCoroutine(FreeRollRoutine());
        }
    }

    private IEnumerator FreeRollRoutine()
    {
        isRolling = true; // Zar dönmeye başladı, kilitle

        // 1. Zarı döndürme animasyonunu başlat 
        if (d20Animator != null) d20Animator.SetInteger("diceResult", 100);
        yield return new WaitForSeconds(rollDuration);

    
        int randomResult = Random.Range(1, 21);
        if (d20Animator != null) d20Animator.SetInteger("diceResult", randomResult);

        Debug.Log("Masada serbest zar atıldı! Gelen Sayı: " + randomResult);

        isRolling = false; 
    }
}
