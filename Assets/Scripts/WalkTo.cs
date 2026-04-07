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

    void Start()
    {
        StartCoroutine(WalkToDesk());
    }

    private IEnumerator WalkToDesk()
    {
        bool hasDesk = false;
        float elapsedTime=0f;
        Vector3 basePos = transform.position;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (!hasDesk)
            {
                basePos = Vector3.MoveTowards(basePos, targetPos.position, walkSpeed*Time.deltaTime);
                if (Vector3.Distance(basePos, targetPos.position) <= 0.1f)
                {
                    hasDesk=true;
                    basePos=targetPos.position;
                    Debug.Log("postacı yürüdü");

                    if(dialoguePanel!=null) 
                    {
                        dialoguePanel.SetActive(true);
                    }
                }
            }

            float yOffset = Mathf.Sin(elapsedTime * bobSpeed) *bobHeight;

        if (!hasDesk)
            {
                yOffset = Mathf.Sin(elapsedTime * (bobSpeed+5)) *(bobHeight+5f) ;
                yield return null;
            }
             
            transform.position=new Vector3(basePos.x, basePos.y+yOffset, basePos.z);

            yield return null;
        }
    }
}
