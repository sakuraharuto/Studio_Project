using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{   
    public ItemData itemData;

    public int id;

    public int onGridPositionX;
    public int onGridPositionY;

    public Transform previousParent;
    public Transform currentParent;

    internal void Set(ItemData itemData)
    {   
        this.itemData = itemData;

        id = itemData.itemID;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2(
            itemData.width * ItemGrid.tileSizeWidth,
            itemData.height * ItemGrid.tileSizeHeight
            );

        GetComponent<RectTransform>().sizeDelta = size;
    }
}
