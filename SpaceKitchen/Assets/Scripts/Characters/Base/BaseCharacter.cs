using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ƶ��ű�

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float walkSpeed = 5f;         //ˮƽ����
    public float sprintSpeed = 8f;       //�����ٶ�
    public float jumpHeight = 2f;        //��Ծ�߶�
    public float gravity = -30f;         //����Ӧ��

    [Header("������")]
    public LayerMask groundLayer;                          //�������ֲ�
    public bool isGrounded;                                //�Ƿ񴥵�

    protected CharacterController controller;
    protected Vector3 velocity;                              //��ɫ�ٶ�����
    protected float currentSpeed;                            //��ǰ�ٶȣ�walk or sprint��

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;                             //��ʼ���ٶ�ΪwalkSpeed
    }

    protected virtual void Update()
    {
        //1.���ؼ��
        CheckGrounded();

        //2.ˮƽ�ƶ�+��Ծ
        HandleMovement();

        //3.����Ӧ��
        ApplyGravity();
    }

    protected virtual void CheckGrounded()
    {
        float radiusReduction = 0.05f;                                 //���滺��ռ䣬��ֹ�Ӵ�ǽ�����
        float verticalOffset = 0.1f;                                   //������������̧ƫ��������ֹ©��
        float rayLength = 0.15f;                                       //���߼�� ���߳���

        //����⣺���ͼ�⣨�����߼����ȶ���
        Vector3 spherePos = transform.position + Vector3.up * verticalOffset;  //����λ��
        bool sphereCheck = Physics.CheckSphere(spherePos, controller.radius - radiusReduction, groundLayer);
        //�����⣺���߼��
        bool rayCheck = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + rayLength, groundLayer);

        isGrounded = sphereCheck || rayCheck;

    }

    protected abstract void HandleMovement();

    public virtual void Move(Vector3 direction)
    {
        if (direction.magnitude > 1f) direction.Normalize();            //������һ��������б���ƶ�ʱ�ٶȹ��죬����һ���Ա��ƻ�
        controller.Move(direction * Time.deltaTime);                    //�������ƶ�����Ծ
    }

    public virtual void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);         //Ϊ�˵���ָ���߶�jumpHeight����ĳ�ʼ��ֱ�ٶ� v^2 = 2gh
        }
    }

    protected virtual void ApplyGravity()
    {
        //����
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;                                           //��΢��ѹ���ִ���
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }
}
