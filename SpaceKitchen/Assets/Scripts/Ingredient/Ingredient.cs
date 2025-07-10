using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//食材基类
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //当前食物类型
    public string displayName;                   //显示名称


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
            case IngredientType.RawProk:
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
        }

        //更新UI
        GetComponentInChildren<TextMesh>().text = displayName;
    }
}
