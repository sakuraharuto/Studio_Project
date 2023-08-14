using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeaving : MonoBehaviour
{
    public GameObject KeyUI;
    public PopUpManager popupManager;// Start is called before the first frame update
    private bool canLeave = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canLeave = true;
            KeyUI.SetActive(true);
        }
           
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLeave = false;
            KeyUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&canLeave)
        {
            popupManager.ShowPopup("LeavingPanel");
        }
    }
}
