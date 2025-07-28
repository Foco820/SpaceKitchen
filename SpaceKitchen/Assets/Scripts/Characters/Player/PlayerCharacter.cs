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
    public Transform holdPoint;            //手持点
    public float interactionDistance = 2f; //交互距离
    public LayerMask interactableLayer;    //可交互物体层

    private Ingredient heldIngredient;     //当前手持食材
    private Kitchenware currentKitchenware;//当前面对厨具
    private Ingredient currentIngredient;  //当前面对食材


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

        // 按下鼠标：尝试拾取
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUp();
        }

        // 松开鼠标：尝试放置
        if (Input.GetMouseButtonUp(0) && heldIngredient != null)
        {
            TryPutDown();
        }

        // 持续持有：更新位置
        if (heldIngredient != null)
        {
            heldIngredient.transform.position = holdPoint.position;
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


    //交互方法
    private void DetectInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);           //检测射线
        RaycastHit hit;                                                        //检测到的物体

        currentKitchenware = null;
        currentIngredient = null;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            currentKitchenware =  hit.collider.GetComponent<Kitchenware>();       //检测厨具   

            if (currentKitchenware != null)                                       //检测食材
            {
                currentIngredient = currentKitchenware.currentIngredient;              //检测到厨具，获取厨具上食材
            }
            else
            {
                currentIngredient = hit.collider.GetComponent<Ingredient>();           //没检测到厨具，检测独立食材

                if (currentIngredient != null && currentIngredient.currentKitchenware != null)    //根据食材倒获取其厨具
                {
                    currentKitchenware = currentIngredient.currentKitchenware;
                }
            }
        }
    }

    private void TryPickUp()
    {
        if (heldIngredient == null && currentIngredient != null)
        {
            heldIngredient = currentIngredient;

            if (heldIngredient.currentKitchenware != null)                 //从厨具上取下
            {
                heldIngredient.currentKitchenware.RemoveIngredient();
            }

            heldIngredient.transform.SetParent(holdPoint);                 //拿手上
            heldIngredient.transform.localPosition = Vector3.zero;

            currentIngredient = null;                                      //清除面前食材
        }
    }

    private void TryPutDown()
    {
        if (heldIngredient == null) return;

        if (currentKitchenware != null)                                     //尝试放置到厨具上
        {
            if (currentKitchenware.PlaceIngredient(heldIngredient))
            {
                heldIngredient = null;
                return;
            }
        }

        DropToGround();                                                      //否则丢地上
    }

    private void DropToGround()
    {
        heldIngredient.transform.SetParent(null);

        // 简单放置：放在玩家前方
        Vector3 dropPos = transform.position + transform.forward * 1f;
        heldIngredient.transform.position = dropPos;

        heldIngredient = null;
    }
}
