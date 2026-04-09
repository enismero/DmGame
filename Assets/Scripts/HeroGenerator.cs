using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class HeroGenerator : MonoBehaviour
{

    [HideInInspector] public string currentHeroName;

    [Header("Layers")]
    public Image bodyLayer;
    public Image headLayer;
    public Image pupilLayer;
    public Image eyeShapeLayer;
    public Image noseLayer;
    public Image mouthLayer;
    public Image beardLayer;
    public Image mustacheLayer;
    public Image hairLayer;
    public Image decorLayer;

    [Header("Sprites")]
    public Sprite defaultBody;
    public Sprite[] faces;       // 5 
    public Sprite[] eyeShapes;   // 5 
    public Sprite[] pupils;      // 1 
    public Sprite[] noses;       // 5 
    public Sprite[] mouths;      // 5 
    public Sprite[] hairs;       // 5  1 
    public Sprite[] beards;      // 4  1 
    public Sprite[] mustaches;   // 4  1 
    public Sprite[] decors;      // 4  1 

    [Header("Colors")]
    public Color[] skinColors;
    public Color[] hairColors;
    public Color[] eyeColors;

    [Header("Names :")]
    public String[] firstNames =
    {
        "Aurelius", "Cassia", "Valerius", "Lucan", "Titus", "Silas", "Julia", "Felix", "Rufina", "Gaius", 
        "Lucia", "Decimus", "Quintus", "Octavia", "Tiberius", "Magnus", "Justina", "Linus", "Ignis", "Caelus", 
        "Alaric", "Balian", "Cedric", "Dante", "Ector", "Gareth", "Hector", "Leofric", "Martel", "Percival", 
        "Roland", "Tristan", "Uther", "Valeria", "Aldous", "Beric", "Cassian", "Dorian", "Eliana", "Finnian", 
        "Gideon", "Hadrian", "Ilias", "Jorah", "Kaelen", "Loriana", "Marius", "Nicias", "Orion", "Phineas",
        "Achilles", "Adonis", "Aeneas", "Ajax", "Apollo", "Ares", "Asclepius", "Atlas", "Cadmus", "Castor", 
        "Chiron", "Daedalus", "Dionysus", "Eros", "Hades", "Hector", "Helios", "Hephaestus", "Heracles", "Hermes", 
        "Icarus", "Jason", "Minos", "Morpheus", "Nestor", "Odysseus", "Orion", "Orpheus", "Pan", "Paris", 
        "Patroclus", "Peleus", "Perseus", "Poseidon", "Prometheus", "Sisyphus", "Tantalus", "Theseus", "Triton", "Zeus", 
        "Athena", "Artemis", "Aphrodite", "Cassandra", "Daphne", "Helen", "Medea", "Penelope", "Persephone", "Psyche"
    };
    public string[] lastNames =
    {
        "Ironclad", "Corvinus", "Blackwood", "Aquila", "Stormrider", "Draconis", "Lightbringer", "Lupus", "Nightshade", "Taurus", 
        "Bellator", "Venator", "Stronghold", "Argentum", "Goldleaf", "Silva", "Stonebridge", "Montis", "Riverrun", "Noctis", 
        "Dawnbringer", "Lunaris", "Sunstrike", "Terra", "Seafarer", "Glacies", "Shadowwalker", "Umbra", "Truthseeker", "Lux", 
        "Swiftfoot", "Fortis", "Ironheart", "Clarus", "Heavyhand", "Velox", "Wisebeard", "Sapiens", "Loyalblood", "Audax", 
        "Invictus", "Aeternus", "Lionheart", "Maximus", "Bearslayer", "Severus", "Falconeye", "Pius", "Wolfhound", "Augustus",
        "Aegis", "Olympian", "Stormcaller", "Earthshaker", "Sunblessed", "Starborn", "Seaborn", "Thunderstrike", "Windrider", "Shadowcaster", 
        "Nightfall", "Dawnstrider", "Fireheart", "Stonecleaver", "Silverbow", "Goldenfleece", "Serpentbane", "Titanblood", "Mythborn", "Fatebinder", 
        "Doomweaver", "Soulcatcher", "Dreamwalker", "Bloodmoon", "Darktide", "Highborn", "Deathless", "Abysswalker", "Lightbringer", "Starfall", 
        "Sunfire", "Moondrop", "Everlasting", "Immortal", "Voidcaller", "Oracle", "Prophet", "Truthspeaker", "Oathkeeper", "Ironfist", 
        "Swiftfoot", "Shieldbearer", "Spearhead", "Warhound", "Bloodaxe", "Gorgonslayer", "Hydrabane", "Myrmidon", "Spartan", "Argonaut"
    };


    public string GenerateVisualsAndName()
    {
        bodyLayer.sprite=defaultBody;
        SetLayer(headLayer,faces);
        SetLayer(pupilLayer,pupils);
        SetLayer(eyeShapeLayer,eyeShapes);
        SetLayer(noseLayer, noses);
        SetLayer(mouthLayer, mouths);
        SetLayer(hairLayer, hairs);
        SetLayer(beardLayer, beards);
        SetLayer(mustacheLayer, mustaches);
        SetLayer(decorLayer, decors);


        Color randomSkinColor=skinColors[Random.Range(0,skinColors.Length)];
        Color randomHairColor = hairColors[Random.Range(0, hairColors.Length)];
        Color randomEyeColor = eyeColors[Random.Range(0, eyeColors.Length)];


        if(headLayer.sprite!=null)headLayer.color = randomSkinColor;
       // if(bodyLayer.sprite!=null)bodyLayer.color = randomSkinColor;

        if(hairLayer.sprite!=null)hairLayer.color = randomHairColor;
        if(beardLayer.sprite!=null)beardLayer.color = randomHairColor;
        if(mustacheLayer.sprite!=null)mustacheLayer.color = randomHairColor;

        if(pupilLayer.sprite!=null)pupilLayer.color = randomEyeColor;


        currentHeroName=firstNames[Random.Range(0,firstNames.Length)]+" "+lastNames[Random.Range(0,lastNames.Length)];
        return currentHeroName;
    }

    private void SetLayer(Image layer, Sprite[] spriteArray)
    {
        if(spriteArray==null || spriteArray.Length==0) return;
        Sprite pickedSprite=spriteArray[Random.Range(0,spriteArray.Length)];
        if (pickedSprite != null)
        {
            layer.sprite=pickedSprite;
            layer.color= Color.white;
        }
        else
        {
            layer.sprite=null;
            layer.color=Color.clear;
        }
    }
}
