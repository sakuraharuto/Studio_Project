using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    // use dictionary to store items count
    public Dictionary<int, int> bagStats;

    // store all items
    public List<ItemData> itemsList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        bagStats = new Dictionary<int, int>();

        // can't use "Resources.LoadAll()". Try another way.
        //itemsList = new List<ItemData>();
    }

    public void Init()
    {
        // Load All items
        //itemsList.AddRange(allItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemData GetItemByID(int id)
    {
        foreach(ItemData item in itemsList) 
        { 
            if(item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }
}
