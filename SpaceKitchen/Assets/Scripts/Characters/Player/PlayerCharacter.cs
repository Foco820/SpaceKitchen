using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("���ר��")]
    //�¶�
    public float crouchHeight = 1f;        //�¶׸߶�

    private float originalHeight;          //��ǰ�߶�
    private bool isCrouching;              //�Ƿ��¶�

    protected override void Start()
    {
        base.Start();
        originalHeight = controller.height;                            //��ʼ������߶�Ϊվ���߶�
    }

    protected override void Update()
    {
        base.Update();

        //�¶�
        Crouch();
    }

    protected override void HandleMovement()
    {
        //����ƶ�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     //ˮƽ�ƶ�����
        Vector3 moveDirection = move * currentSpeed;
        moveDirection.y = velocity.y;                                   //�ϲ�ˮƽ�ʹ�ֱ�ƶ�
        Move(moveDirection);

        //����
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //�����Ծ
        if (Input.GetButtonDown("Jump")) Jump();
    }

    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching? crouchHeight : originalHeight;
        }
    }
}
