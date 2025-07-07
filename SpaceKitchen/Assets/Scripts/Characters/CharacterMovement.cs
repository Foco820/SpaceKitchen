using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ƶ��ű�

public class CharacterMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    [SerializeField] public float walkSpeed = 5f;         //ˮƽ����
    [SerializeField] public float sprintSpeed = 8f;       //�����ٶ�
    [SerializeField] public float jumpHeight = 2f;        //��Ծ�߶�
    [SerializeField] public float gravity = -30f;       //����Ӧ��

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
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)  velocity.y = -2f;  //��΢��ѹ���ִ���

        //2.ˮƽ�ƶ�
        HandleMovement();

        //3.��Ծ
        HandleJump();

        //4.����Ӧ��
        ApplyGravity();
    }

    void CheckGround()
    {
        //����⣺���ͼ�⣨�����߼����ȶ���

        //�����⣺���߼��

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
