using UnityEngine;


public enum HeroClass // enum ile yeni veri tipi tanımladık
{
        Knight,Wizard,Rogue,Bard
}

public enum StatType { Strength, Dexterity, Intelligence, Charisma }

public struct HeroStats  //struct ile tek bi paket yaptık
{
    public string heroName;
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
    

    [Header("Managment referance")]
    public UIManager uiManager;
    public HeroGenerator heroGenerator;

    [Header("Hero Card Settings")]
    public GameObject heroCardPrefab; 
    public Transform deskArea;        
    private GameObject currentHeroCard;


    public HeroStats newHero;


    void Start()
    {
        SpawnRandomHero();
    }

    public void SpawnRandomHero()
    {
        newHero= new HeroStats();

    //generate visual and name
        if (heroGenerator != null)
        {
            newHero.heroName=heroGenerator.GenerateVisualsAndName();
        }
        else
        {
            newHero.heroName="unknown hero";
        }

    //generate class and stats
        int heroCount = System.Enum.GetValues(typeof(HeroClass)).Length;
        //select random hero class
        newHero.heroClass= (HeroClass)Random.Range(0,heroCount);

        //average stats
        newHero.strength= Random.Range(1,6);
        newHero.dexterity= Random.Range(1,6);
        newHero.intelligence= Random.Range(1,6);
        newHero.charisma= Random.Range(1,6);

        //specific stats
        switch (newHero.heroClass)
        {
            case HeroClass.Knight: //+STR-INT
                newHero.strength=Random.Range(7,11);
                newHero.intelligence=Random.Range(1,4);
                newHero.goldAmount = Random.Range(50, 150);
                break;

            case HeroClass.Wizard: //+INT-STR
                newHero.strength=Random.Range(1,4);
                newHero.intelligence=Random.Range(7,11);
                newHero.goldAmount = Random.Range(150, 300);
                break;
            
            case HeroClass.Rogue: //+DEX~CHA
                newHero.dexterity=Random.Range(7, 11);
                newHero.charisma = Random.Range(4, 8);
                newHero.goldAmount = Random.Range(300, 600);
                break;

            case HeroClass.Bard: //+CHA~DEX
                newHero.dexterity=Random.Range(4, 8);
                newHero.charisma = Random.Range(7, 11);
                newHero.goldAmount = Random.Range(200, 450);
                break;
        }

        newHero.maxHealth= 10+ (newHero.strength*2);
        

        Debug.Log("--Masaya biri geldi--");
        Debug.Log($"<class {newHero.heroClass}> Str: {newHero.strength} | Dex: {newHero.dexterity} | Int: {newHero.intelligence} | Cha: {newHero.charisma} \n Hp: {newHero.maxHealth} | Gold: {newHero.goldAmount}");


        uiManager.UpdateHeroUI(newHero);
    }

    public void SpawnHeroCardOnDesk()
    {
        // Masada zaten bir kart varsa üst üste binmesin diye eskisini sil
        if (currentHeroCard != null) Destroy(currentHeroCard);

        // Yeni kartı yarat
        currentHeroCard = Instantiate(heroCardPrefab, deskArea);
        
        // Boyutunun küçülmesini engelle (Görünmez olmaması için)
        currentHeroCard.transform.localScale = Vector3.one; 
        
        // Havadan düşmesi için yüksekten başlat
        currentHeroCard.transform.localPosition = new Vector3(0, 25, 0); 
        
        Debug.Log("Kahraman kimliği masaya fırlatıldı!");
    }

    // 3. KİMLİK KARTINI SİLME (HeroDropZone, adam gidince burayı çağırır)
    public void DestroyHeroCard()
    {
        if (currentHeroCard != null)
        {
            Destroy(currentHeroCard);
        }
    }
}
