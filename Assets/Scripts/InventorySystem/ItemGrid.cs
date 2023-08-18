using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{   
    // tile config
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32; 

    RectTransform rectTransform;
    
    Vector2 positionOnGrid;
    Vector2Int tileGridPosition = new Vector2Int();

    // Create Grid
    InventoryItem[,] inventoryItemSlot;

    // Grid config
    [SerializeField] int gridSizeWidth = 5;
    [SerializeField] int gridSizeHeight = 10;

    // offset
    public const float scaleFactor = 1.43625f;
    //public const float scaleFactor = 1.65625f;

    // Start is called before the first frame update 
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); 
        Init();
    }

    public InventoryItem PickUpItem(int x, int y)
    {   
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if(toReturn == null) { return null; }
        
        CleanGridReference(toReturn);
        
        return toReturn;
    }

    // pick up item, the grid on panel will not be occupied.
    private void CleanGridReference(InventoryItem item)
    {
        for(int ix = 0; ix < item.itemData.width; ix++)
        {
            for(int iy = 0; iy < item.itemData.height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    public void Init()
    {   
        rectTransform = GetComponent<RectTransform>();
        inventoryItemSlot = new InventoryItem[gridSizeWidth, gridSizeHeight];
        Vector2 size = new Vector2(gridSizeWidth * tileSizeWidth, gridSizeHeight * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }
    
    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {   
        positionOnGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - mousePosition.y;
        
        tileGridPosition.x = (int)(positionOnGrid.x / (tileSizeWidth * scaleFactor));
        tileGridPosition.y = (int)(positionOnGrid.y / (tileSizeHeight * scaleFactor));

        // tileGridPosition.x = (int)(positionOnGrid.x / (tileSizeWidth));
        // tileGridPosition.y = (int)(positionOnGrid.y / (tileSizeHeight));

        return tileGridPosition;
    }

    public Vector2Int? FindSpaceForObject(ItemData itemData)
    {   
        int height = gridSizeHeight - itemData.height + 1;
        int width = gridSizeWidth - itemData.width + 1;
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x, y, itemData.width, itemData.height) == true)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {   
        if(BoundaryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height) == false)
        {
            return false;
        }

        if(OverlapCheck(posX, posY, 
                            inventoryItem.itemData.width, 
                            inventoryItem.itemData.height, 
                            ref overlapItem) == false)
        {   
            overlapItem = null;
            return false;
        }
        
        if(overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }
    
    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        // occupy grids on the panel
        for(int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for(int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }
 
        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.height / 2);
        return position;
    }

    public bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {   
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX+x, posY+y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX+x, posY+y];
                    }
                    else
                    {   
                        if(overlapItem != inventoryItemSlot[posX+x, posY+y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {   
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX+x, posY+y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) { return false; }

        posX += width-1;
        posY += height-1;

        if(PositionCheck(posX, posY) == false) { return false; }

        return true;
    }

}
