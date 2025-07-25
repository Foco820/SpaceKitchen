using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���߻���
public class Kitchenware : MonoBehaviour
{
    public Ingredient currentIngredient;          //��ǰ�����ϵ�ʳ��

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //����ʳ��
    public virtual bool PlaceIngredient(Ingredient ingredient)
    {
        if (currentIngredient == null)                           //������Ϊ�գ�һ��ֻ�ܷ���һ��ʳ�ģ�����Ҫ�ģ�
        {
            currentIngredient = ingredient;
            //��ʳ���ƶ��������Ϸ�
            ingredient.transform.position = transform.position + Vector3.up * .2f;

            //���߹���
            StartUsing();
            
            return true;
        }
        return false;
    }

    //�Ƴ�ʳ��
    public virtual void RemoveIngredient()
    {
        currentIngredient = null;
    }

    protected virtual void StartUsing()
    {
        //������߾���ʵ��
    }
}
