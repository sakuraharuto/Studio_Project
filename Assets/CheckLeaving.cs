using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeaving : MonoBehaviour
{
    public PopUpManager popupManager;// Start is called before the first frame update
    private bool canLeave = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canLeave = true;
        }
           
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLeave = false;
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
