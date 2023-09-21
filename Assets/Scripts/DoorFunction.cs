using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DoorFunction : MonoBehaviour
{
    public Transform Player;

    [SerializeField]
    public GameObject Canvas;
    [Header("Door Rotation Config")]
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private float rotationAmount = 0f;
    private float rotationDirection = 0f;

    // check for animation
    private bool Opened = false;
    private Vector3 StartRotation;
    private Vector3 Forward;
    private Coroutine AnimCoroutine;

    // check door status for path finder
    bool isOpened;
    bool isLocked;

    // Start is called before the first frame update
    void Start()
    {   
        // check for later use
        isOpened = false;
        // isLocked = true;

        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.right;

    }

    // Update is called once per frame
    void Update()
    {
        if (Canvas.activeSelf)
        {
            if ((!isOpened || isLocked) && Input.GetKeyUp(KeyCode.R))
            {

                OpenButton();
            }
            else if (isOpened && Input.GetKeyUp(KeyCode.R))
            {
                if (Opened)
                {
                    CloseDoor();
                }

            }

        }
     
    }

    public void OpenButton()
    {
        // Debug.Log("Open");
        
        Canvas.SetActive(false);
        OpenDoor(Player.position);

        //Destroy(GetComponent<NavMeshObstacle>(), 0.5f);
    }

    public void OpenDoor(Vector3 UserPosition)
    {

        if (!Opened)
        {
            if (AnimCoroutine != null)
            {
                StopCoroutine(AnimCoroutine);
            }

            float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
            AnimCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Debug.Log("Door is Openning");

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if(ForwardAmount >= rotationDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + rotationAmount, 0));    
        }

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }

        isOpened = true;
        Opened = true;
    }

    public void CloseDoor()
    {
        if(Opened)
        {
            if(AnimCoroutine != null)
            {
                StopCoroutine (AnimCoroutine);
            }

            AnimCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Debug.Log("Door is Closing");

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        Opened = false;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<CharacterController>(out CharacterController Controller))
        {   
            // first time or locked, show the button
            if (!isOpened || isLocked)
            {
                Canvas.SetActive(true);
            }

            // opened before, automatically open the door
            if (isOpened)
            {
                OpenDoor(Player.position);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        
        Canvas.SetActive(false);
        if(other.TryGetComponent<CharacterController>(out CharacterController Controller))
        {
            if(Opened)
            {
                CloseDoor();
            }
        }
        
    }

 

}
