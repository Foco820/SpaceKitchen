using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基础移动脚本

public class CharacterMovement : MonoBehaviour
{
    [Header("移动参数")]
    [SerializeField] public float walkSpeed = 5f;         //水平移速
    [SerializeField] public float sprintSpeed = 8f;       //疾跑速度
    [SerializeField] public float jumpHeight = 2f;        //跳跃高度
    [SerializeField] public float gravity = -30f;       //重力应用

    private CharacterController controller;
    private Vector3 velocity;                              //角色速度向量
    private bool isGrounded;                               //是否触地
    private float currentSpeed;                            //当前速度（walk or sprint）

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;                             //初始化速度为walkSpeed
    }

    void Update()
    {
        //1.触地检测
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)  velocity.y = -2f;  //轻微下压保持触地

        //2.水平移动
        HandleMovement();

        //3.跳跃
        HandleJump();

        //4.重力应用
        ApplyGravity();
    }

    void CheckGround()
    {
        //主检测：球型检测（比射线检测更稳定）

        //补充检测：射线检测

    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     //水平移动向量
        Vector3 totalMove = move * currentSpeed;
        totalMove.y = velocity.y;                                       //合并水平和垂直移动

        //疾跑
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(totalMove * Time.deltaTime);                    //发生：移动和跳跃
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);         //为了到达指定高度jumpHeight所需的初始垂直速度 v^2 = 2gh
        }
    }

    void ApplyGravity()
    {
        //触地
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;                                           //轻微下压保持触地
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }
}
