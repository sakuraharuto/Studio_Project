using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f; //移动速度
    public GameObject feedbackUI; // UI反馈对象

    private float defaultZPosition;
    private bool isMoving = false;

    [Header("Function Setting")]
    public GameObject searchPoint;
    
    [HideInInspector] public GameObject teleportDoor;

    private void Start()
    {   
        defaultZPosition = transform.position.z;
    }

    private void Update()
    {
        if (teleportDoor) 
        {
            Debug.Log("已赋值" + teleportDoor);
        }

        MoveCharacter();
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); //获取玩家的横向输入

        if(horizontal != 0)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0f, 0f).normalized; //创建移动方向
            Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime; //计算目标位置

            transform.position = targetPosition;
            transform.forward = moveDirection;
            isMoving = true;

            ShowFeedbackUI(targetPosition);
        }
        else if(isMoving)
        {
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

    public GameObject GetSearchPoint()
    {
        return searchPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = other.gameObject;
        }

        if(other.CompareTag("SearchPoint"))
        {
            searchPoint = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = null;
        }

        if (other.CompareTag("SearchPoint"))
        {   
            searchPoint = null;
        }
    }
}
