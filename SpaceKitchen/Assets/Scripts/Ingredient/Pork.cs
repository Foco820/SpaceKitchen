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
        currentType = IngredientType.RawPork;
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
        if (chopCount >= 5 && currentType == IngredientType.RawPork)
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
}
