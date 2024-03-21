using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    [Header("Time System Config")]
    public float worldTimer;
    public float dayTimeSpeed;

    [Header("Characters Config")]
    public GameObject player;
    public Character enemy;

    /// <summary>
    /// PopupManager
    /// menu ui manager
    /// </summary>
    public PopUpManager popupManager;  // 将 PopupManager 的实例拖放到此处

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        

    }

    void Update()
    {   
        UpdateDayLight();




        // UI interact
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            popupManager.ShowPopup("SettingPanel");
        }
    }

    private void UpdateDayLight()
    {
        //update daytime
        // rotate environment light by TimeDelta.time * daylightSpeed
    }

    private void UpdateInGameDate()
    {
        //update date
        // add 1 day by each 360 rotation of daylight
    }

}
