using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//÷Ì»‚¿‡
public class Pork : Ingredient
{
    public int chopCount = 0;                 //«–≤À¥Œ ˝º∆ ˝∆˜

    void Start()
    {
        //≥ı ºªØ÷Ì»‚¿‡ ≥≤ƒ√˚≥∆Œ™…˙÷Ì»‚
        currentType = IngredientType.RawProk;
        UpdateDisplayName();
    }

    void Update()
    {
        
    }

    //«–
    public void Chop()
    {
        chopCount++;

        //…˙÷Ì»‚ => ÷Ì≈≈
        if (chopCount >= 5 && currentType == IngredientType.RawProk)
        {
            currentType = IngredientType.PorkChop;
            chopCount = 0;
        }
        //÷Ì≈≈ => ÷Ì»‚√”
        else if (chopCount >= 4 && currentType == IngredientType.PorkChop)
        {
            currentType = IngredientType.MincedPork;
            chopCount = 0;
        }

        UpdateDisplayName();
    }

    //øæ
    public void Cook()
    {
        //÷Ì»‚ => øæ÷Ì»‚
        if (currentType == IngredientType.RawProk)
        {
            currentType = IngredientType.CookedPork;
        }
        //÷Ì≈≈ => øæ÷Ì≈≈
        else if (currentType == IngredientType.PorkChop)
        {
            currentType = IngredientType.CookedPorkChop;
        }

        UpdateDisplayName();
    }
}
