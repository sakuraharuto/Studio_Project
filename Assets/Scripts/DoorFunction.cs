using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DoorFunction : MonoBehaviour
{
    [SerializeField]
    public GameObject Canvas;
    public Button OpenButton;
    public Button UnlockButton;


    public bool isOpened;
    public bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        isLocked = true;
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

        Destroy(GetComponent<NavMeshObstacle>());
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (!isOpened)
        { 
            if (other.gameObject.name == "Player")
            {
                Debug.Log("Show Door UI");
                Canvas.SetActive(true);
            }
        }

        //if (isLocked)
        //{

        //}
    }
}
