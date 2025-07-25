using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//砧板
public class CuttingBoard : Kitchenware
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //执行切
    public void ChopIngredient()
    {
        if (currentIngredient != null && currentIngredient is Pork pork)
        {
            pork.Chop();
        } 
    }

    protected override void StartUsing()
    {
        //砧板不为自动厨具，等待手动交互
    }
}
