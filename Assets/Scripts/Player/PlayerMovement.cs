using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f; //移动速度
    public GameObject feedbackUI; // UI反馈对象

    private float defaultZPosition;
    private bool isMoving = false;
    private bool canTeleport = false;
    private bool canOpenDoor = false;

    [Header("Function Setting")]



    private Rigidbody rb; // 添加Rigidbody引用

    [HideInInspector] public GameObject teleportDoor;

    //public static int t = 0;
    public SearchPoint sp;


    private void Start()
    {
        defaultZPosition = transform.position.z;
        rb = GetComponent<Rigidbody>(); // 初始化Rigidbody引用

        sp = null;

        ItemStats.instance.Init();
    }

    private void Update()
    {
        if (teleportDoor)
        {
            Debug.Log("已赋值" + teleportDoor);
        }

        MoveCharacter();

        if (Input.GetKeyUp(KeyCode.W)&&canTeleport) {
            Teleport();

        }


    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); //获取玩家的横向输入

        if (horizontal != 0)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0f, 0f).normalized; //创建移动方向
            Vector3 moveVelocity = moveDirection * moveSpeed;

            // 保持y轴速度，以考虑重力
            moveVelocity.y = rb.velocity.y;

            rb.velocity = moveVelocity;

            transform.forward = moveDirection;
            isMoving = true;

            ShowFeedbackUI(transform.position + moveDirection);
        }
        else if (isMoving)
        {
            Vector3 stopVelocity = new Vector3(0, rb.velocity.y, 0);
            rb.velocity = stopVelocity;

            isMoving = false;
            HideFeedbackUI();
        }
    }

    void ShowFeedbackUI(Vector3 position)
    {
        feedbackUI.transform.position = position;
        feedbackUI.SetActive(true);
    }

    void HideFeedbackUI()
    {
        feedbackUI.SetActive(false);
    }

    // 上下楼传送
    public void Teleport()
    {
        transform.position = teleportDoor.GetComponent<TeleportPoint>().GetTeleportLocation().position;
        Debug.Log("Teleport to location: " + transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = other.gameObject;
            canTeleport = true;
        }

        if(other.CompareTag("SearchPoint"))
        {
            sp = other.GetComponent<SearchPoint>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = null;
            canTeleport = false;
        }

        if (other.CompareTag("SearchPoint"))
        {
            sp = null;
        }
    }
}
