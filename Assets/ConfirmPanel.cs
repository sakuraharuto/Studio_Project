using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmPanel : MonoBehaviour
{
    public string buildingName;
    public TMP_Text notifyText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        notifyText.text = "Do you want to enter: " + buildingName + "?";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConfirmEnter()
    {
        //do transition scene
        GameSceneManager.instance.StartTransition(buildingName);
        gameObject.SetActive(false);
    }

    public void CancelEnter()
    {
        gameObject.SetActive(false);
    }
}
