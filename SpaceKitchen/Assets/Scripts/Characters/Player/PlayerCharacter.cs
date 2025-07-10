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
    private Ingredient heldIngredient;     //��ǰ�ֳ�ʳ��


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
        if (Input.GetMouseButtonDown(0))
        {
            if (heldIngredient == null)
            {
                //����ʰȡ
                TryPickUp();
            }
            else
            {
                //����ʹ��
                TryInteract();
            }
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


    private void DetectInteractable()
    {
        
    }

    private void TryPickUp()
    {

    }

    private void TryInteract()
    {

    }
}
