using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PopUpManager popupManager;  // 将 PopupManager 的实例拖放到此处

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            popupManager.ShowPopup("SettingPanel");
        }
        
    }

}
