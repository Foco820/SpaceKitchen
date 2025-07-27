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

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            currentKitchenware =  hit.collider.GetComponent<Kitchenware>();       //������   

            if (heldIngredient == null)
            {
                Ingredient ingredient = hit.collider.GetComponent<Ingredient>();  //���ʳ��

                if (ingredient != null)
                {
                    //������ʾ�ɽ���ʳ��
                }
            }
        }
        else
        {
            currentKitchenware = null;
        }
    }

    private void TryPickUp()
    {

    }

    private void TryPutDown()
    {

    }
}
