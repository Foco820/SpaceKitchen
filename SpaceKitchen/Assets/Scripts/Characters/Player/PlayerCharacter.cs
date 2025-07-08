using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("玩家专属")]
    //下蹲
    public float crouchHeight = 1f;        //下蹲高度

    private float originalHeight;          //当前高度
    private bool isCrouching;              //是否下蹲

    protected override void Start()
    {
        base.Start();
        originalHeight = controller.height;                            //初始化人物高度为站立高度
    }

    protected override void Update()
    {
        base.Update();

        //下蹲
        Crouch();
    }

    protected override void HandleMovement()
    {
        //玩家移动
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;     //水平移动向量
        Vector3 moveDirection = move * currentSpeed;
        moveDirection.y = velocity.y;                                   //合并水平和垂直移动
        Move(moveDirection);

        //疾跑
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //玩家跳跃
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
