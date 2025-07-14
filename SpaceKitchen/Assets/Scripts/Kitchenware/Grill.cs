using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//烤架
//连续烤制系统（双重烧焦路径）
public class Grill : Kitchenware
{
    //烤制阶段结构
    public class CookingStage
    {
        public IngredientType requireInput;    //输入食材
        public IngredientType output;          //输出食材
        public float time;                     //烤制时间
    }

    public List<CookingStage> cookingStages = new List<CookingStage>();      //烤制阶段List
    private int currentStage = -1;             //当前阶段  （currentStage = -1 表示未烹饪阶段,如 无食材，刚放置食材未处理，完成烹饪，烧焦完成）
    private float timer;                       //阶段计时器          

    [Header("烧焦")]
    public float defaultBurnTime = 5f;         //烧焦所需时间
    private bool isBurning = false;            //是否烧焦（不匹配List食材）阶段

    void Start()
    {
        //猪肉类
        cookingStages.Add(new CookingStage                  //生猪肉 => 烤猪肉
        {
            requireInput = IngredientType.RawPork,
            output = IngredientType.CookedPork,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //猪排 => 烤猪排
        {
            requireInput = IngredientType.PorkChop,
            output = IngredientType.CookedPorkChop,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //烤猪肉 => 烧焦的食物
        {
            requireInput = IngredientType.CookedPork,
            output = IngredientType.BurntFood,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //烤猪排 => 烧焦的食物
        {
            requireInput = IngredientType.CookedPorkChop,
            output = IngredientType.BurntFood,
            time = 5f
        });
    }

    void Update()
    {
        if (currentIngredient != null)                                     //厨具上有食材，烤制
        {
            //烧焦路径1
            if (isBurning)                                          //烧焦（不在阶段List中的食材）
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    currentIngredient.Burn();            
                    isBurning = false;                   
                    currentStage = -1;                   
                }
            }

            //检测阶段
            if (currentStage == -1 || currentIngredient.currentType != cookingStages[currentStage].requireInput)
                                                                    //初始阶段 或 食材与当前阶段输入食材不匹配（比如玩家更换了食材） 
            {
                bool foundStage = false;                      //是否检测到合适阶段

                for (int i = 0; i < cookingStages.Count; i++) //遍历寻找合适阶段
                {
                    if (cookingStages[i].requireInput == currentIngredient.currentType)
                    {
                        currentStage = i;
                        timer = cookingStages[i].time;
                        foundStage = true;
                        break;
                    }
                }

                if (!foundStage)                              //没有找到合适阶段
                {
                    isBurning = true;
                    timer = defaultBurnTime;
                    return;
                }
            }

            //烹饪计时
            timer -= Time.deltaTime;

            //阶段转变
            if (timer <= 0)
            {
                if (cookingStages[currentStage].output == IngredientType.BurntFood)
                                                                    //烧焦路径2          
                {
                    currentIngredient.Burn();                 //烧焦（在阶段List中的食材）        
                }
                else                                                //烤制路径
                {
                    currentIngredient.currentType = cookingStages[currentStage].output;
                    currentIngredient.UpdateDisplayName();    //更新食材
                }

                //移动到下一个阶段
                if (currentStage < cookingStages.Count - 1)
                {
                    currentStage++;
                    timer = cookingStages[currentStage].time;
                }
                else                                                //List里的最后一个结构体   
                {
                    currentStage = -1;
                }
            }
        }
        else                                                               //厨具上没有食材，不烤制
        {
            currentStage = -1;                                      //初始阶段
            isBurning = false;                     
        }
    }
}
