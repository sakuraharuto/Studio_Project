using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    [Header("Time System Config")]
    public float worldTimer;
    public float dayTimeSpeed;

    [Header("Player Config")]
    public GameObject player;

    [Header("Monster Config")]
    //public Unit monster;
    public CombatUnit monster;


    /// <summary>
    /// PopupManager
    /// Idk what this is... dont touch
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

        //Initial Character States
        //Character.instance.Init();
    }

    void Start()
    {
        

    }

    void Update()
    {   
        UpdateDayLight();






        // dk what this is for
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
