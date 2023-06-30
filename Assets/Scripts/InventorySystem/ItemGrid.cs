using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{   
    // tile config
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32; 

    RectTransform rectTransform;
    
    // UI Panel
    InventoryItem[,] inventoryItemSlot;

    // package config
    [SerializeField]
    int gridSizeWidth = 5;
    [SerializeField]
    int gridSizeHeight = 10;

    //offset
    public const float scaleFactor = 1.43625f;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); 
        Init(gridSizeWidth, gridSizeHeight);

    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        for(int ix = 0; ix < toReturn.itemData.width; ix++)
        {
            for(int iy = 0; iy < toReturn.itemData.height; iy++)
            {
                inventoryItemSlot[toReturn.onGridPositionX + ix, toReturn.onGridPositionY + iy] = null;
            }
        }
        
        return toReturn;
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {   
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;
        
        tileGridPosition.x = (int)(positionOnGrid.x / (tileSizeWidth * scaleFactor));
        tileGridPosition.y = (int)(positionOnGrid.y / (tileSizeHeight * scaleFactor));

        return tileGridPosition;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for(int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for(int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
                
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.height / 2); 

        rectTransform.localPosition = position;
    }









}
