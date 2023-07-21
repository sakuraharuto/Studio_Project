using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DoorFunction : MonoBehaviour
{
    [SerializeField]
    public Transform Player;
    public GameObject Canvas;
    public Button OpenButton;
    public Button UnlockButton;
    
    Animation DoorAnim;

    bool isOpened;
    bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        isLocked = true;
        DoorAnim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        Debug.Log("Door is open");

        isOpened = true;
        Canvas.SetActive(false);

        // Door open to Right
        //if (Player.position.x > transform.position.x)
        //{
            //Debug.Log("Open to right");
            //DoorAnim.Play("DoorOpen_cw");

        //}
        //// Door open to Left
        //if (Player.position.x < transform.position.x)
        //{

        //}

        Destroy(GetComponent<NavMeshObstacle>(), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isLocked)
        //{

        //}

        //Debug.Log(other.gameObject.name);
        if (!isOpened)
        { 
            if (other.gameObject.name == "Player")
            {
                Debug.Log("Show Door UI");
                Canvas.SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }
}
