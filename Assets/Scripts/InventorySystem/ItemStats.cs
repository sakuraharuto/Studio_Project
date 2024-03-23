using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    /// <summary>
    /// Item Stats: ID, Count
    /// </summary>
    public Dictionary<int, int> bagStats;
    // all items
    [SerializeField] private ItemData[] allItems;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            allItems = Resources.LoadAll<ItemData>("Items");
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        bagStats = new Dictionary<int, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemData GetItemByID(int id)
    {
        foreach (ItemData item in allItems)
        { 
            if(item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }

    public int RandomItemID()
    {
        int id = Random.Range(1, allItems.Length);

        return id;
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


