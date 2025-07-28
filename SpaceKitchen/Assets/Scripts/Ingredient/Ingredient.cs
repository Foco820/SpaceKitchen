using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʳ�Ļ���
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //��ǰʳ������
    public string displayName;                   //��ʾ����

    public Kitchenware currentKitchenware;       //��ǰ���ڳ���

    [Header("�ս�")]
    public bool isBurnt = false;                 //�Ƿ��ս�


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
            case IngredientType.RawPork:
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
            case IngredientType.BurntFood:
                displayName = "�ս���ʳ��";
                break;
        }

        //����UI
        GetComponentInChildren<TextMesh>().text = displayName;
    }

    //ʳ���ս�
    public virtual void Burn()
    {
        if (!isBurnt)                    //��ֹ�ظ��ս�
        {
            currentType = IngredientType.BurntFood;
            isBurnt = true;
            UpdateDisplayName();         //�ս�������������ʾ
        }
    }

    //�����õ������ϵ���
    public void PlaceOnKitchenware(Kitchenware kitchenware)
    {
        currentKitchenware = kitchenware;
    }

    //��ȡ�ߵ���
    public void RemoveFromKitchenware()
    {
        if (currentKitchenware != null)
        {
            currentKitchenware.RemoveIngredient();
            currentKitchenware = null;
        }
    }
}
