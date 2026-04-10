using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    public bool isRolling=false;
    public int finalDiceValue=0;

    [Header("Dice Settings")]
    public float throwForce=500f;
    public float rollSpeed=500f;

    private Vector3 startingPosition;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        startingPosition=transform.position;
        rb.useGravity=false;

    }

    void Update()
    {
        if(isRolling && rb.linearVelocity.sqrMagnitude<0.01f && rb.angularVelocity.sqrMagnitude < 0.01f)
        {
            if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            {
                isRolling=false;
                CalculateDiceValue();
            }
            
            
        }
    }

    public void RollDice()
    {
        finalDiceValue=0;
        isRolling=true;
        rb.useGravity=true;

        transform.position=startingPosition;
        transform.rotation=Random.rotation;

        rb.AddForce(Vector3.up*throwForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere*rollSpeed,ForceMode.Impulse);
    }

    private void CalculateDiceValue()
    {
        float[] dots=new float[6];
        dots[0]=Vector3.Dot(transform.up,Vector3.up);
        dots[1]=Vector3.Dot(-transform.up,Vector3.up);
        dots[2]=Vector3.Dot(transform.right,Vector3.up);
        dots[3]=Vector3.Dot(-transform.right,Vector3.up);
        dots[4]=Vector3.Dot(transform.forward,Vector3.up);
        dots[5]=Vector3.Dot(-transform.forward,Vector3.up);

        int maxIndex=0;
        float maxValue=dots[0];

        for(int i=1;i<6;i++)
        {
            if (dots[i] > maxValue)
            {
                maxValue=dots[i];
                maxIndex=i;
            }
        }

        if (maxIndex == 0) finalDiceValue = 1;
        else if (maxIndex == 1) finalDiceValue = 6;
        else if (maxIndex == 2) finalDiceValue = 2;
        else if (maxIndex == 3) finalDiceValue = 5;
        else if (maxIndex == 4) finalDiceValue = 3;
        else if (maxIndex == 5) finalDiceValue = 4;

        Debug.Log("gelen değer"+finalDiceValue);
    }
}
