using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIBase : MonoBehaviour
{   
    UIManager uiManager;
    public virtual void Open()
    {
        gameObject.SetActive(true); 
    }

    public virtual void Close() 
    {
        gameObject.SetActive(false);
    }

    public virtual void Destroy()
    {
        uiManager.DestroyUI(gameObject.name);
    }
}
