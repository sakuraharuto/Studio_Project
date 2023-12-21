using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{   
    public ItemData itemData;

    public int onGridPositionX;
    public int onGridPositionY;

    public int count = 0;

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon; 

        Vector2 size = new Vector2(
            itemData.width * ItemGrid.tileSizeWidth * ItemGrid.scaleFactor, 
            itemData.height * ItemGrid.tileSizeHeight * ItemGrid.scaleFactor
            );

        //Vector2 size = new Vector2(
        //    itemData.width * ItemGrid.tileSizeWidth,
        //    itemData.height * ItemGrid.tileSizeHeight
        //    );
        //size.x = itemData.width * ItemGrid.tileSizeWidth;
        //size.y = itemData.height * ItemGrid.tileSizeHeight;

        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void AddCount()
    {
        count++;
    }
}
