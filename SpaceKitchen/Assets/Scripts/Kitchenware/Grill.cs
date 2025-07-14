using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����
//��������ϵͳ��˫���ս�·����
public class Grill : Kitchenware
{
    //���ƽ׶νṹ
    public class CookingStage
    {
        public IngredientType requireInput;    //����ʳ��
        public IngredientType output;          //���ʳ��
        public float time;                     //����ʱ��
    }

    public List<CookingStage> cookingStages = new List<CookingStage>();      //���ƽ׶�List
    private int currentStage = -1;             //��ǰ�׶�  ��currentStage = -1 ��ʾδ��⿽׶�,�� ��ʳ�ģ��շ���ʳ��δ���������⿣��ս���ɣ�
    private float timer;                       //�׶μ�ʱ��          

    [Header("�ս�")]
    public float defaultBurnTime = 5f;         //�ս�����ʱ��
    private bool isBurning = false;            //�Ƿ��ս�����ƥ��Listʳ�ģ��׶�

    void Start()
    {
        //������
        cookingStages.Add(new CookingStage                  //������ => ������
        {
            requireInput = IngredientType.RawPork,
            output = IngredientType.CookedPork,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //���� => ������
        {
            requireInput = IngredientType.PorkChop,
            output = IngredientType.CookedPorkChop,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //������ => �ս���ʳ��
        {
            requireInput = IngredientType.CookedPork,
            output = IngredientType.BurntFood,
            time = 5f
        });

        cookingStages.Add(new CookingStage                  //������ => �ս���ʳ��
        {
            requireInput = IngredientType.CookedPorkChop,
            output = IngredientType.BurntFood,
            time = 5f
        });
    }

    void Update()
    {
        if (currentIngredient != null)                                     //��������ʳ�ģ�����
        {
            //�ս�·��1
            if (isBurning)                                          //�ս������ڽ׶�List�е�ʳ�ģ�
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    currentIngredient.Burn();            
                    isBurning = false;                   
                    currentStage = -1;                   
                }
            }

            //���׶�
            if (currentStage == -1 || currentIngredient.currentType != cookingStages[currentStage].requireInput)
                                                                    //��ʼ�׶� �� ʳ���뵱ǰ�׶�����ʳ�Ĳ�ƥ�䣨������Ҹ�����ʳ�ģ� 
            {
                bool foundStage = false;                      //�Ƿ��⵽���ʽ׶�

                for (int i = 0; i < cookingStages.Count; i++) //����Ѱ�Һ��ʽ׶�
                {
                    if (cookingStages[i].requireInput == currentIngredient.currentType)
                    {
                        currentStage = i;
                        timer = cookingStages[i].time;
                        foundStage = true;
                        break;
                    }
                }

                if (!foundStage)                              //û���ҵ����ʽ׶�
                {
                    isBurning = true;
                    timer = defaultBurnTime;
                    return;
                }
            }

            //��⿼�ʱ
            timer -= Time.deltaTime;

            //�׶�ת��
            if (timer <= 0)
            {
                if (cookingStages[currentStage].output == IngredientType.BurntFood)
                                                                    //�ս�·��2          
                {
                    currentIngredient.Burn();                 //�ս����ڽ׶�List�е�ʳ�ģ�        
                }
                else                                                //����·��
                {
                    currentIngredient.currentType = cookingStages[currentStage].output;
                    currentIngredient.UpdateDisplayName();    //����ʳ��
                }

                //�ƶ�����һ���׶�
                if (currentStage < cookingStages.Count - 1)
                {
                    currentStage++;
                    timer = cookingStages[currentStage].time;
                }
                else                                                //List������һ���ṹ��   
                {
                    currentStage = -1;
                }
            }
        }
        else                                                               //������û��ʳ�ģ�������
        {
            currentStage = -1;                                      //��ʼ�׶�
            isBurning = false;                     
        }
    }
}
