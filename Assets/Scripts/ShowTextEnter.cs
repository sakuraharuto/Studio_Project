using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowTextEnter : MonoBehaviour
{
    private TMP_Text showText;
    // Start is called before the first frame update
    void Start()
    {
        showText = this.GetComponent<TMP_Text>();
        showText.text = "Enter "+ transform.parent.GetComponent<DialogEnter>().buildingName+" ?";
    }

    
}
