using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//ʳ�Ļ���
public class Ingredient : MonoBehaviour
{
    public IngredientType currentType;           //��ǰʳ������

    [Header("�ı���ʾ")]
    public string displayName;                   //��ʾ����
    private TextMeshPro tmpComponent;            //TextMeshPro�ı����

    public Kitchenware currentKitchenware;       //��ǰ���ڳ���

    [Header("�ս�")]
    public bool isBurnt = false;                 //�Ƿ��ս�


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //�ı����������
        if (tmpComponent != null && Camera.main != null)
        {
            Vector3 lookDirection = Camera.main.transform.position - tmpComponent.transform.position;
            lookDirection.y = 0;                                                        //�ȶ��ı�y��

            tmpComponent.transform.rotation = Quaternion.LookRotation(lookDirection);   //�����������ת
        }
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

        //����TextMeshPro�����UI
        UpdateTextMeshPro();
    }

    private void UpdateTextMeshPro()
    {
        EnsureTextMeshProComponent();                 //ȷ���ı��������

        if (tmpComponent != null)
        {
            tmpComponent.text = displayName;          //�����ı�
        } 
    }

    private void EnsureTextMeshProComponent()
    {
        tmpComponent = GetComponentInChildren<TextMeshPro>();      //��ȡ�ı����

        if (tmpComponent == null)                                  //û���ı���������
        {
            CreateTextMeshProObject();                                 //�����ı��������
        }
    }

    private void CreateTextMeshProObject()
    {
        //�����ı���������
        GameObject textobj = new GameObject("TMP_Label");
        textobj.transform.SetParent(transform);                     //���������ı������������
        textobj.transform.localPosition = Vector3.up * 0.5f;        //�ı�������λ�ã�ʳ���Ϸ�

        //���tmp���
        tmpComponent = textobj.AddComponent<TextMeshPro>();

        //����
        ConfigureTextComponent();
    }

    private void ConfigureTextComponent()
    {
        tmpComponent.fontSize = 24;
        tmpComponent.alignment = TextAlignmentOptions.Center;
        tmpComponent.enableWordWrapping = false;
        tmpComponent.sortingOrder = 100;                             // ȷ����3D�����Ϸ�
        tmpComponent.fontStyle = FontStyles.Bold;
        tmpComponent.color = Color.white;

        tmpComponent.enableVertexGradient = true;                    // ��ӽ���Ч��
        tmpComponent.colorGradient = new VertexGradient(
            Color.white,
            Color.white,
            new Color(0.8f, 0.8f, 0.8f),
            new Color(0.8f, 0.8f, 0.8f)
        );

        tmpComponent.outlineWidth = 0.2f;                             // �������
        tmpComponent.outlineColor = Color.black;
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
