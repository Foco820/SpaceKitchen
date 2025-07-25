using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//厨具基类
public class Kitchenware : MonoBehaviour
{
    public Ingredient currentIngredient;          //当前厨具上的食材

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //放置食材
    public virtual bool PlaceIngredient(Ingredient ingredient)
    {
        if (currentIngredient == null)                           //厨具上为空（一次只能放置一个食材，后续要改）
        {
            currentIngredient = ingredient;
            //将食材移动到厨具上方
            ingredient.transform.position = transform.position + Vector3.up * .2f;

            //厨具功能
            StartUsing();
            
            return true;
        }
        return false;
    }

    //移除食材
    public virtual void RemoveIngredient()
    {
        currentIngredient = null;
    }

    protected virtual void StartUsing()
    {
        //具体厨具具体实现
    }
}
