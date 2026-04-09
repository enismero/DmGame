using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class HeroGenerator : MonoBehaviour
{
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

    [Header("names")]
    public String[] firstName={"random"};
    public string[] lastName={"generate"};


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


        string generateName=firstName[Random.Range(0,firstName.Length)]+" "+lastName[Random.Range(0,lastName.Length)];
        return generateName;
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
