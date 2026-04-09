using UnityEngine;
using System.Collections;

public class WalkTo : MonoBehaviour
{


    [Header("Move Settings")]
        public Transform targetPos;
        public float walkSpeed=500f;
    [Header("Bobing Settings")]
        public float bobHeight=5f;
        public float bobSpeed=5f;
    [Header("Diyalog Ayarları")]
        public GameObject dialoguePanel;
        public GameObject heroObject;

    private Vector3 startPos;
    private Vector3 currentGoal;
    private bool isAtTarget=false;
    private float elapsedTime=0f;
    private Vector3 basePos;


    void Start()
    {
        startPos = transform.position;
        currentGoal = targetPos.position;
        basePos = transform.position;
        
    }


    public void GoBack()
    {
        currentGoal = startPos;
        isAtTarget = false;
        Debug.Log("geri dönüyor...");
    }


        void Update()
        {
            elapsedTime += Time.deltaTime;

            if (!isAtTarget)
            {
                basePos = Vector3.MoveTowards(basePos, currentGoal , walkSpeed*Time.deltaTime);
                if (Vector3.Distance(basePos, currentGoal) <= 0.1f)
                {
                    isAtTarget=true;
                    basePos=currentGoal;
                    Debug.Log("yürüdü");

                    if(currentGoal==targetPos.position)
                    {
                        Debug.Log("masaya ulaştı.");
                        if(dialoguePanel!=null) dialoguePanel.SetActive(true);
                    }
                    else if(currentGoal==startPos)
                    {
                        Debug.Log("döndü");
                        gameObject.SetActive(false);
                        if(heroObject!=null) heroObject.SetActive(true);  
                    }
                }
            }
            float currentBobSpeed=isAtTarget?bobSpeed:bobSpeed+5f;
            float currentBobHeight=isAtTarget?bobHeight:bobHeight+5f;
            float yOffset = Mathf.Sin(elapsedTime * currentBobSpeed) *currentBobHeight;
             
            transform.position=new Vector3(basePos.x, basePos.y+yOffset, basePos.z);
        }
}
