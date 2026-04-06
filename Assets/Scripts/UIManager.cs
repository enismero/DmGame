using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [Header("UI referance")]
    public TextMeshProUGUI classNameText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;


    public void UpdateHeroUI(HeroStats hero)
    {
        classNameText.text="Class: "+hero.heroClass.ToString();
        statsText.text=$"Str: {hero.strength} | Dex: {hero.dexterity} | Int: {hero.intelligence} | Cha: {hero.charisma}";
        healthText.text=$"Hp: {hero.maxHealth}";
        goldText.text=$"Gold: {hero.goldAmount}";
    }
}
