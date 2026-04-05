using UnityEngine;

public enum HeroClass // enum ile yeni veri tipi tanımladık
{
    Knight,
    Wizard,
    Rogue,
    Bard
}

public struct HeroStats  //struct ile tek bi paket yaptık
{
    public HeroClass heroClass;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;
    public int maxHealth;
    public int goldAmount;
}


public class GameManager : MonoBehaviour
{
    void Start()
    {
        SpawnRandomHero();
    }

    public void SpawnRandomHero()
    {
        int heroCount = System.Enum.GetValues(typeof(HeroClass)).Length;

        HeroStats newHero= new HeroStats();
        //select random hero class
        newHero.heroClass= (HeroClass)Random.Range(0,heroCount);

        newHero.strength= Random.Range(1,11);
        newHero.dexterity= Random.Range(1,11);
        newHero.intelligence= Random.Range(1,11);
        newHero.charisma= Random.Range(1,11);

        newHero.maxHealth= 10+ (newHero.strength*2);
        newHero.goldAmount=Random.Range(50,501);

        Debug.Log("--Masaya biri geldi--");
        Debug.Log($"class {newHero.heroClass}");
        Debug.Log($"Str: {newHero.strength} | Dex: {newHero.dexterity} | Int: {newHero.intelligence} | Cha: {newHero.charisma} \n Hp: {newHero.maxHealth} | Gold: {newHero.goldAmount}");

    }
}
