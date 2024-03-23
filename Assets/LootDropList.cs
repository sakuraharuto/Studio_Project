using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LootDropList : UIBase
{
    public GameObject lootBoard;
    public GameObject lootSlot;

    // Start is called before the first frame update
    void Start()
    {   
        GenerateLootSlots();
    }

    public void GenerateLootSlots()
    {
        for (int i = 0; i < CombatManager.instance.lootList.Count; i++)
        {   
            GameObject slot = Instantiate(lootSlot, lootBoard.transform);

            slot.GetComponent<Image>().sprite = ItemStats.instance.GetItemByID(CombatManager.instance.lootList[i]).itemIcon;
        }
    }

}
