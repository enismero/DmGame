using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MoneyPouch : MonoBehaviour
{
   [Header("kese ayarı")]
   public TMP_InputField goldInputField; //klavye giriş yeri
   public int totalGoldInPouch=0;
   private CanvasGroup canvasGroup;
   private Vector3 initialPos; //kesenin yeri


   void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        initialPos = transform.position;

        //yazı alanı
        if (goldInputField != null)
        {
            goldInputField.contentType=TMP_InputField.ContentType.IntegerNumber;
            goldInputField.onValueChanged.AddListener(UpdateGoldAmount);
        }
    }

    private void UpdateGoldAmount(string input)
    {
        if(int.TryParse(input,out int result))
        {
            totalGoldInPouch=result;
        }
        else
        {
            totalGoldInPouch=0;
        }
    }
    public void IncreaseGold(int amount)
    {
        totalGoldInPouch += amount;
        UpdateInputField();
    }

    public void DecreaseGold(int amount)
    {
        totalGoldInPouch -= amount;
        if (totalGoldInPouch < 0) totalGoldInPouch = 0; // Para eksiye düşmesin
        UpdateInputField();
    }

    private void UpdateInputField()
    {
        if (goldInputField != null)
        {
            goldInputField.text=totalGoldInPouch.ToString();
        }
    }

    

    public void ResetPouch()
    {
        totalGoldInPouch=0;
        UpdateInputField();
        transform.position=initialPos;
    }
}
