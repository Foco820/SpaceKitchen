using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
public class Grill : Kitchenware
{
    public float cooTime = 5f;              //����ʱ��
    private float cookTimer;                //���ܼ�ʱ��
    private bool isCooking;                 //�Ƿ���

    void Start()
    {
        
    }

    void Update()
    {
        if (currentIngredient != null && !isCooking)            //��������ʳ����û�п������ƣ���ִ�п��ƣ���ʼ���Ƽ�ʱ
        {
            isCooking = true;                
            cookTimer = cooTime;
        }

        if (isCooking)
        {
            cookTimer -= Time.deltaTime;

            if (cookTimer <= 0)                                 //����ʱ�䵽
            {
                if (currentIngredient is Pork pork)
                {
                    pork.Cook();    
                }

                isCooking=false;                                //ֹͣ��ʱ
            }
        }

        if (currentIngredient == null)
        {
            isCooking = false;                                  //������û��ʳ�ģ�������
        }
    }

    //void Update()
    //{
    //    if (currentIngredient != null && !isCooking)            //��������ʳ����û�п������ƣ���ִ�п��ƣ���ʼ���Ƽ�ʱ
    //    {
    //        isCooking = true;
    //        cookTimer = cooTime;
    //    }

    //    if (isCooking)
    //    {
    //        cookTimer -= Time.deltaTime;

    //        if (cookTimer <= 0)                                 //����ʱ�䵽
    //        {
    //            if (currentIngredient is Pork pork)
    //            {
    //                pork.Cook();
    //            }

    //            isCooking = false;                                //ֹͣ��ʱ
    //        }
    //    }

    //    if (currentIngredient == null)
    //    {
    //        isCooking = false;                                  //������û��ʳ�ģ�������
    //    }
    //}
}
