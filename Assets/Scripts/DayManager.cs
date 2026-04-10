using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum DayState {Close,PostmanPart,CustomerPart,DayEnd}
public class DayManager : MonoBehaviour
{
    public DayState currentState=DayState.Close;
    public float dayDurationInSecond= 60f;
    private float currentTime;


    [Header("referance")]
    public GameObject postmanObject;
    public GameObject taskBoardObject;



    public void OpenShop()
    {
        currentState=DayState.PostmanPart;
        SpawnPostman();
        Debug.Log("shop is open ,waiting the postman...");

    }
    public void SpawnPostman()
    {
        postmanObject.SetActive(true);
        Debug.Log("postman is here");
    }
    public void LeavePostman()
    {
        postmanObject.SetActive(false);
        currentState=DayState.CustomerPart;
        Debug.Log("postman was leaved, customers are here");
    }

    
    
    void Update()
    {
        currentTime+=Time.deltaTime;
        if (currentTime >= dayDurationInSecond)
        {
            currentState=DayState.DayEnd;
            EndDay();
        }
    }
    void EndDay()
    {
        currentState=DayState.Close;

        DraggablePaper[] allPapers = Object.FindObjectsByType<DraggablePaper>(FindObjectsSortMode.None);
    
        foreach (var paper in allPapers)
        {
            paper.isNew = false; // Artık hiçbiri "Yeni" değil, iade edilemezler!
        }
        
        
    }

    public void CloseShop()
    {
        currentState=DayState.Close;
        Debug.Log("next day...");
    }
}
