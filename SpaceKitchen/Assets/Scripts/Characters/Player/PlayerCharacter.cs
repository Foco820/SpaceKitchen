using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("玩家专属")]
    public float crouchHeight = 1f;        //下蹲高度

    private float originalHeight;          //当前高度
    private bool isCrouching;              //是否下蹲

    [Header("交互系统")]
    private Ingredient heldIngredient;     //当前手持食材


    protected override void Start()
    {
        base.Start();
        originalHeight = controller.height; //初始高度
    }

    protected override void Update()
    {
        base.Update();

        //下蹲
        if (Input.GetKeyDown(KeyCode.LeftControl)) Crouch();


        //检测交互
        DetectInteractable();
        if (Input.GetMouseButtonDown(0))
        {
            if (heldIngredient == null)
            {
                //尝试拾取
                TryPickUp();
            }
            else
            {
                //尝试使用
                TryInteract();
            }
        }
    }



    protected override void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        Vector3 movement = moveDirection * currentSpeed;                             // 应用水平移动速度
        movement.y = velocity.y;                                                     // 合并垂直速度（包含跳跃和重力影响）
        Move(movement * Time.deltaTime);

        //疾跑
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //跳跃
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
