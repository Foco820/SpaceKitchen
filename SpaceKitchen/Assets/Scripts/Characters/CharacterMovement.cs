using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ƶ��ű�

public class CharacterMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float walkSpeed = 5f;         //ˮƽ����
    public float sprintSpeed = 8f;       //�����ٶ�
    public float jumpHeight = 2f;        //��Ծ�߶�
    public float gravity = -30f;         //����Ӧ��

    [Header("������")]
    public LayerMask groundLayer;                          //�������ֲ�

    private CharacterController controller;
    private Vector3 velocity;                              //��ɫ�ٶ�����
    private bool isGrounded;                               //�Ƿ񴥵�
    private float currentSpeed;                            //��ǰ�ٶȣ�walk or sprint��

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;                             //��ʼ���ٶ�ΪwalkSpeed
    }

    void Update()
    {
        //1.���ؼ��
        CheckGrounded();

        //2.ˮƽ�ƶ�
        HandleMovement();

        //3.��Ծ
        HandleJump();

        //4.����Ӧ��
        ApplyGravity();
    }

    void CheckGrounded()
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

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     //ˮƽ�ƶ�����
        Vector3 totalMove = move * currentSpeed;
        totalMove.y = velocity.y;                                       //�ϲ�ˮƽ�ʹ�ֱ�ƶ�

        //����
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(totalMove * Time.deltaTime);                    //�������ƶ�����Ծ
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);         //Ϊ�˵���ָ���߶�jumpHeight����ĳ�ʼ��ֱ�ٶ� v^2 = 2gh
        }
    }

    void ApplyGravity()
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
