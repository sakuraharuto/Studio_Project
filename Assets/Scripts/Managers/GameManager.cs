using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    [Header("Time System Config")]
    public float worldTimer;
    public float dayTimeSpeed;

    [Header("Characters Config")]
    public GameObject player;
    public GameObject enemy;
    public int enemyIndex;
    public CombatUnit postCombatPlayerStats;

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
        //UpdateDayLight();
        if(GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player");
        }


        // UI interact
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            popupManager.ShowPopup("SettingPanel");
        }

        if(enemy != null)
        {
            enemyIndex = enemy.GetComponent<Enemy>().enemyIndex;
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
