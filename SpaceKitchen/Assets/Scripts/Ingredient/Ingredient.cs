using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//食材基类
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //当前食物类型

    [Header("文本显示")]
    public string displayName;                   //显示名称
    private TextMeshPro tmpComponent;            //TextMeshPro文本组件

    public Kitchenware currentKitchenware;       //当前所在厨具

    [Header("烧焦")]
    public bool isBurnt = false;                 //是否烧焦


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //文本面向摄像机
        if (tmpComponent != null && Camera.main != null)
        {
            Vector3 lookDirection = Camera.main.transform.position - tmpComponent.transform.position;
            lookDirection.y = 0;                                                        //稳定文本y轴

            tmpComponent.transform.rotation = Quaternion.LookRotation(lookDirection);   //跟随摄像机旋转
        }
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

        //更新TextMeshPro组件的UI
        UpdateTextMeshPro();
    }

    private void UpdateTextMeshPro()
    {
        EnsureTextMeshProComponent();                 //确保文本组件存在

        if (tmpComponent != null)
        {
            tmpComponent.text = displayName;          //更新文本
        } 
    }

    private void EnsureTextMeshProComponent()
    {
        tmpComponent = GetComponentInChildren<TextMeshPro>();      //获取文本组件

        if (tmpComponent == null)                                  //没有文本组件的情况
        {
            CreateTextMeshProObject();                                 //创建文本组件物体
        }
    }

    private void CreateTextMeshProObject()
    {
        //负责文本的子物体
        GameObject textobj = new GameObject("TMP_Label");
        textobj.transform.SetParent(transform);                     //创建承载文本组件的子物体
        textobj.transform.localPosition = Vector3.up * 0.5f;        //文本子物体位置：食材上方

        //添加tmp组件
        tmpComponent = textobj.AddComponent<TextMeshPro>();

        //配置
        ConfigureTextComponent();
    }

    private void ConfigureTextComponent()
    {
        tmpComponent.fontSize = 24;
        tmpComponent.alignment = TextAlignmentOptions.Center;
        tmpComponent.enableWordWrapping = false;
        tmpComponent.sortingOrder = 100;                             // 确保在3D物体上方
        tmpComponent.fontStyle = FontStyles.Bold;
        tmpComponent.color = Color.white;

        tmpComponent.enableVertexGradient = true;                    // 添加渐变效果
        tmpComponent.colorGradient = new VertexGradient(
            Color.white,
            Color.white,
            new Color(0.8f, 0.8f, 0.8f),
            new Color(0.8f, 0.8f, 0.8f)
        );

        tmpComponent.outlineWidth = 0.2f;                             // 添加轮廓
        tmpComponent.outlineColor = Color.black;
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
