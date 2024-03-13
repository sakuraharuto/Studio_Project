using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    // use dictionary to store items count
    public Dictionary<int, int> bagStats;
    // all items
    [SerializeField] private ItemData[] allItems;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }
    }

    private void LoadPackageStats(SaveData data)
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {   
        bagStats = new Dictionary<int, int>();

        //allItems = new ItemData[3];
    }

    public void Init()
    {
        // Load All items
        //allItems = Resources.LoadAll<ItemData>("Resources/Items/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemData GetItemByID(int id)
    {
        //foreach (ItemData item in itemsList) 
        foreach (ItemData item in allItems)
        { 
            if(item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct InventoryStatsData
{
    public ItemStats itemStats;

    public InventoryStatsData(ItemStats _itemStats)
    {
        itemStats = _itemStats;
    }
}


