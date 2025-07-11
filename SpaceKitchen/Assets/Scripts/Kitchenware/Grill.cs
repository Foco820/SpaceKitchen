using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//烤架
public class Grill : Kitchenware
{
    public float cooTime = 5f;              //烤制时间
    private float cookTimer;                //烤架计时器
    private bool isCooking;                 //是否烤制

    void Start()
    {
        
    }

    void Update()
    {
        if (currentIngredient != null && !isCooking)            //烤架上有食材且没有开启烤制，则执行烤制，开始烤制计时
        {
            isCooking = true;                
            cookTimer = cooTime;
        }

        if (isCooking)
        {
            cookTimer -= Time.deltaTime;

            if (cookTimer <= 0)                                 //烤制时间到
            {
                if (currentIngredient is Pork pork)
                {
                    pork.Cook();    
                }

                isCooking=false;                                //停止计时
            }
        }

        if (currentIngredient == null)
        {
            isCooking = false;                                  //烤架上没有食材，不烤制
        }
    }

    //void Update()
    //{
    //    if (currentIngredient != null && !isCooking)            //烤架上有食材且没有开启烤制，则执行烤制，开始烤制计时
    //    {
    //        isCooking = true;
    //        cookTimer = cooTime;
    //    }

    //    if (isCooking)
    //    {
    //        cookTimer -= Time.deltaTime;

    //        if (cookTimer <= 0)                                 //烤制时间到
    //        {
    //            if (currentIngredient is Pork pork)
    //            {
    //                pork.Cook();
    //            }

    //            isCooking = false;                                //停止计时
    //        }
    //    }

    //    if (currentIngredient == null)
    //    {
    //        isCooking = false;                                  //烤架上没有食材，不烤制
    //    }
    //}
}
