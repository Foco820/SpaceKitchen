using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("���ר��")]
    public float crouchHeight = 1f;        //�¶׸߶�

    private float originalHeight;          //��ǰ�߶�
    private bool isCrouching;              //�Ƿ��¶�

    [Header("����ϵͳ")]
    public Transform holdPoint;            //�ֳֵ�
    public float interactionDistance = 2f; //��������
    public LayerMask interactableLayer;    //�ɽ��������

    private Ingredient heldIngredient;     //��ǰ�ֳ�ʳ��
    private Kitchenware currentKitchenware;//��ǰ��Գ���
    private Ingredient currentIngredient;  //��ǰ���ʳ��


    protected override void Start()
    {
        base.Start();
        originalHeight = controller.height; //��ʼ�߶�
    }

    protected override void Update()
    {
        base.Update();

        //�¶�
        if (Input.GetKeyDown(KeyCode.LeftControl)) Crouch();


        //��⽻��
        DetectInteractable();

        // ������꣺����ʰȡ
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUp();
        }

        // �ɿ���꣺���Է���
        if (Input.GetMouseButtonUp(0) && heldIngredient != null)
        {
            TryPutDown();
        }

        // �������У�����λ��
        if (heldIngredient != null)
        {
            heldIngredient.transform.position = holdPoint.position;
        }
    }



    protected override void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        Vector3 movement = moveDirection * currentSpeed;                             // Ӧ��ˮƽ�ƶ��ٶ�
        movement.y = velocity.y;                                                     // �ϲ���ֱ�ٶȣ�������Ծ������Ӱ�죩
        Move(movement * Time.deltaTime);

        //����
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //��Ծ
        if (Input.GetButtonDown("Jump")) Jump();
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
        controller.height = isCrouching ? crouchHeight : originalHeight;
    }


    //��������
    private void DetectInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);           //�������
        RaycastHit hit;                                                        //��⵽������

        currentKitchenware = null;
        currentIngredient = null;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            currentKitchenware =  hit.collider.GetComponent<Kitchenware>();       //������   

            if (currentKitchenware != null)                                       //���ʳ��
            {
                currentIngredient = currentKitchenware.currentIngredient;              //��⵽���ߣ���ȡ������ʳ��
            }
            else
            {
                currentIngredient = hit.collider.GetComponent<Ingredient>();           //û��⵽���ߣ�������ʳ��

                if (currentIngredient != null && currentIngredient.currentKitchenware != null)    //����ʳ�ĵ���ȡ�����
                {
                    currentKitchenware = currentIngredient.currentKitchenware;
                }
            }
        }
    }

    private void TryPickUp()
    {
        if (heldIngredient == null && currentIngredient != null)
        {
            heldIngredient = currentIngredient;

            if (heldIngredient.currentKitchenware != null)                 //�ӳ�����ȡ��
            {
                heldIngredient.currentKitchenware.RemoveIngredient();
            }

            heldIngredient.transform.SetParent(holdPoint);                 //������
            heldIngredient.transform.localPosition = Vector3.zero;

            currentIngredient = null;                                      //�����ǰʳ��
        }
    }

    private void TryPutDown()
    {
        if (heldIngredient == null) return;

        if (currentKitchenware != null)                                     //���Է��õ�������
        {
            if (currentKitchenware.PlaceIngredient(heldIngredient))
            {
                heldIngredient = null;
                return;
            }
        }

        DropToGround();                                                      //���򶪵���
    }

    private void DropToGround()
    {
        heldIngredient.transform.SetParent(null);

        // �򵥷��ã��������ǰ��
        Vector3 dropPos = transform.position + transform.forward * 1f;
        heldIngredient.transform.position = dropPos;

        heldIngredient = null;
    }
}
