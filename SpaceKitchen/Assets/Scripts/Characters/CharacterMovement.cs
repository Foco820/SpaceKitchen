using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ƶ��ű�

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;         //ˮƽ����
    [SerializeField] private float jumpHeight = 2f;        //��Ծ�߶�
    [SerializeField] private float gravity = -9.81f;       //����Ӧ��

    private CharacterController controller;
    private Vector3 velocity;                              //��ɫ�ٶ�����
    private bool isGrounded;                               //�Ƿ񴥵�

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //1.���ؼ��
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)  velocity.y = -2f;  //��΢��ѹ���ִ���

        //2.ˮƽ�ƶ�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z; 

        controller.Move(move * moveSpeed * Time.deltaTime);

        //3.��Ծ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); //Ϊ�˵���ָ���߶�jumpHeight����ĳ�ʼ��ֱ�ٶ� v^2 = 2gh
        }

        //4.����Ӧ��
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
