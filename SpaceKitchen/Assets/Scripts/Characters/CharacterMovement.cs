using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基础移动脚本

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;         //水平移速
    [SerializeField] private float jumpHeight = 2f;        //跳跃高度
    [SerializeField] private float gravity = -9.81f;       //重力应用

    private CharacterController controller;
    private Vector3 velocity;                              //角色速度向量
    private bool isGrounded;                               //是否触地

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //1.触地检测
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)  velocity.y = -2f;  //轻微下压保持触地

        //2.水平移动
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z; 

        controller.Move(move * moveSpeed * Time.deltaTime);

        //3.跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); //为了到达指定高度jumpHeight所需的初始垂直速度 v^2 = 2gh
        }

        //4.重力应用
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
