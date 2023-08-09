using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    public float moveSpeed = 5f; // 移动速度
    public GameObject feedbackUI; // UI反馈对象

    private float defaultZPosition;
    private bool isMoving = false;

    [SerializeField]public GameObject teleportDoor;

    private void Start()
    {   
        defaultZPosition = transform.position.z;
        agent.speed = moveSpeed; // 设置 NavMeshAgent 的移动速度
    }

    private void Update()
    {
        if (teleportDoor) {
            Debug.Log("已赋值" + teleportDoor);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)&&hit.collider.gameObject.layer==3)
            {
                Vector3 targetPosition = hit.point;
                targetPosition = hit.point;
                targetPosition.z = defaultZPosition;

                agent.SetDestination(targetPosition);
                ShowFeedbackUI(targetPosition);
                isMoving = true;
            }
        }

        if (isMoving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
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
        GetComponent<NavMeshAgent>().enabled = false;
            transform.position = teleportDoor.GetComponent<TeleportPoint>().GetTeleportLocation().position;
        GetComponent<NavMeshAgent>().enabled = true;
        agent.SetDestination(transform.position);

            Debug.Log("Teleport to location: " + transform.position);
        
     
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("TeleportDoor"))
        {
            teleportDoor = null;
        }
    }
}