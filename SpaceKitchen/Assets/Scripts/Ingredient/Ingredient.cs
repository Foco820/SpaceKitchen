using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//食材基类
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //当前食物类型
    public string displayName;                   //显示名称

    public Kitchenware currentKitchenware;       //当前所在厨具

    [Header("烧焦")]
    public bool isBurnt = false;                 //是否烧焦


    void Start()
    {
        
    }

    void Update()
    {
        //名称显示
        UpdateDisplayName();
    }

    public void UpdateDisplayName()
    {
        //根据类型显示名称
        switch (currentType)
        {
            case IngredientType.RawPork:
                displayName = "生猪肉";
                break;
            case IngredientType.PorkChop:
                displayName = "猪排";
                break;
            case IngredientType.MincedPork:
                displayName = "猪肉糜";
                break;
            case IngredientType.CookedPork:
                displayName = "烤猪肉";
                break;
            case IngredientType.CookedPorkChop:
                displayName = "烤猪排";
                break;
            case IngredientType.BurntFood:
                displayName = "烧焦的食物";
                break;
        }

        //更新UI
        GetComponentInChildren<TextMesh>().text = displayName;
    }

    //食物烧焦
    public virtual void Burn()
    {
        if (!isBurnt)                    //防止重复烧焦
        {
            currentType = IngredientType.BurntFood;
            isBurnt = true;
            UpdateDisplayName();         //烧焦后立即更新显示
        }
    }

    //被放置到厨具上调用
    public void PlaceOnKitchenware(Kitchenware kitchenware)
    {
        currentKitchenware = kitchenware;
    }

    //被取走调用
    public void RemoveFromKitchenware()
    {
        if (currentKitchenware != null)
        {
            currentKitchenware.RemoveIngredient();
            currentKitchenware = null;
        }
    }
}
