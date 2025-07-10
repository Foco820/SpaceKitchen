using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//’Ë∞Â
public class CuttingBoard : Kitchenware
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //÷¥––«–
    public void ChopIngredient()
    {
        if (currentIngredient != null && currentIngredient is Pork pork)
        {
            pork.Chop();
        } 
    }
}
