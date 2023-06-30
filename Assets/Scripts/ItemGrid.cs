using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{   
    // tile config
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32; 

    RectTransform rectTransform;
    
    // UI Panel
    InventoryItem[,] inventoryItemSlot;

    // package config
    [SerializeField]
    int gridSizeWidth = 5;
    [SerializeField]
    int gridSizeHeight = 10;

    [SerializeField]
    GameObject itemPrefab;

    //offset
    float scaleFactor = 1.43625f;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); 
        Init(gridSizeWidth, gridSizeHeight);

        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 0, 0);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];
        inventoryItemSlot[x, y] = null;
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
        inventoryItemSlot[posX, posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight / 2); 

        rectTransform.localPosition = position;
    }









}
