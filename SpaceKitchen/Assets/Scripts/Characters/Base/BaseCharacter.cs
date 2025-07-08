using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基础移动脚本

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("移动参数")]
    public float walkSpeed = 5f;         //水平移速
    public float sprintSpeed = 8f;       //疾跑速度
    public float jumpHeight = 2f;        //跳跃高度
    public float gravity = -30f;         //重力应用

    [Header("地面检测")]
    public LayerMask groundLayer;                          //地面遮罩层
    public bool isGrounded;                                //是否触地

    protected CharacterController controller;
    protected Vector3 velocity;                              //角色速度向量
    protected float currentSpeed;                            //当前速度（walk or sprint）

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;                             //初始化速度为walkSpeed
    }

    protected virtual void Update()
    {
        //1.触地检测
        CheckGrounded();

        //2.水平移动+跳跃
        HandleMovement();

        //3.重力应用
        ApplyGravity();
    }

    protected virtual void CheckGrounded()
    {
        float radiusReduction = 0.05f;                                 //侧面缓冲空间，防止接触墙壁误检
        float verticalOffset = 0.1f;                                   //球体检测球心上抬偏移量，防止漏检
        float rayLength = 0.15f;                                       //射线检测 射线长度

        //主检测：球型检测（比射线检测更稳定）
        Vector3 spherePos = transform.position + Vector3.up * verticalOffset;  //球心位置
        bool sphereCheck = Physics.CheckSphere(spherePos, controller.radius - radiusReduction, groundLayer);
        //补充检测：射线检测
        bool rayCheck = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + rayLength, groundLayer);

        isGrounded = sphereCheck || rayCheck;

    }

    protected abstract void HandleMovement();

    public virtual void Move(Vector3 direction)
    {
        if (direction.magnitude > 1f) direction.Normalize();            //向量归一化，避免斜角移动时速度过快，操作一致性被破坏
        controller.Move(direction * Time.deltaTime);                    //发生：移动和跳跃
    }

    public virtual void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);         //为了到达指定高度jumpHeight所需的初始垂直速度 v^2 = 2gh
        }
    }

    protected virtual void ApplyGravity()
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
