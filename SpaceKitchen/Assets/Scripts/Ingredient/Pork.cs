using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������
public class Pork : Ingredient
{
    public int chopCount = 0;                 //�в˴���������

    void Start()
    {
        //��ʼ��������ʳ������Ϊ������
        currentType = IngredientType.RawProk;
        UpdateDisplayName();
    }

    void Update()
    {
        
    }

    //��
    public void Chop()
    {
        chopCount++;

        //������ => ����
        if (chopCount >= 5 && currentType == IngredientType.RawProk)
        {
            currentType = IngredientType.PorkChop;
            chopCount = 0;
        }
        //���� => ������
        else if (chopCount >= 4 && currentType == IngredientType.PorkChop)
        {
            currentType = IngredientType.MincedPork;
            chopCount = 0;
        }

        UpdateDisplayName();
    }

    //��
    public void Cook()
    {
        //���� => ������
        if (currentType == IngredientType.RawProk)
        {
            currentType = IngredientType.CookedPork;
        }
        //���� => ������
        else if (currentType == IngredientType.PorkChop)
        {
            currentType = IngredientType.CookedPorkChop;
        }

        UpdateDisplayName();
    }
}
