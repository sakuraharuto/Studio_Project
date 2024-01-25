using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    // use dictionary to store items count
    public Dictionary<int, int> bagStats;
    // all items
    [SerializeField] private ItemData[] allItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

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
