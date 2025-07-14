using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ÖíÈâÀà
public class Pork : Ingredient
{
    public int chopCount = 0;                 //ÇÐ²Ë´ÎÊý¼ÆÊýÆ÷

    void Start()
    {
        //³õÊ¼»¯ÖíÈâÀàÊ³²ÄÃû³ÆÎªÉúÖíÈâ
        currentType = IngredientType.RawPork;
        UpdateDisplayName();
    }

    void Update()
    {
        
    }

    //ÇÐ
    public void Chop()
    {
        chopCount++;

        //ÉúÖíÈâ => ÖíÅÅ
        if (chopCount >= 5 && currentType == IngredientType.RawPork)
        {
            currentType = IngredientType.PorkChop;
            chopCount = 0;
        }
        //ÖíÅÅ => ÖíÈâÃÓ
        else if (chopCount >= 4 && currentType == IngredientType.PorkChop)
        {
            currentType = IngredientType.MincedPork;
            chopCount = 0;
        }

        UpdateDisplayName();
    }
}
