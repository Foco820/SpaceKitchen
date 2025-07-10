using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʳ�Ļ���
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //��ǰʳ������
    public string displayName;                   //��ʾ����


    void Start()
    {
        
    }

    void Update()
    {
        //������ʾ
        UpdateDisplayName();
    }

    public void UpdateDisplayName()
    {
        //����������ʾ����
        switch (currentType)
        {
            case IngredientType.RawProk:
                displayName = "������";
                break;
            case IngredientType.PorkChop:
                displayName = "����";
                break;
            case IngredientType.MincedPork:
                displayName = "������";
                break;
            case IngredientType.CookedPork:
                displayName = "������";
                break;
            case IngredientType.CookedPorkChop:
                displayName = "������";
                break;
        }

        //����UI
        GetComponentInChildren<TextMesh>().text = displayName;
    }
}
