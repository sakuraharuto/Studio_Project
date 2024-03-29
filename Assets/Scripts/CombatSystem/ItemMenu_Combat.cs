using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemMenu_Combat : UIBase
{
    public static ItemMenu_Combat instance;

    [Header("Menu Config")]
    [SerializeField] private GameObject slotMenu;
    public ItemSlot_Combat[] itemSlots;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {   
        if(ItemStats.instance.bagStats != null) 
        { 
            InitialItemSlots();
        }
        gameObject.SetActive(false);
    }

    private void InitialItemSlots()
    {
        int slot = 0;
        foreach (KeyValuePair<int, int> pair in ItemStats.instance.bagStats)
        {
            // Get item info
            int itemID = pair.Key;
            int itemCount = pair.Value;
            // Find the item
            ItemData itemData = ItemStats.instance.GetItemByID(itemID);
            //may need to check item category
            //only add items into slots, not equippments.

            // Assign slot
            ItemSlot_Combat itemSlot = itemSlots[slot];
            if (itemSlot != null)
            {
                // Init itemSlot
                itemSlot.SetItemSlot(itemData, itemCount);
            }
            slot++;
        }
    }

}
