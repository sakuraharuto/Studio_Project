//All interaction the player will do with all the inventory grids

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{   
    [HideInInspector]
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid {
        get => selectedItemGrid;
        set {
            selectedItemGrid = value;
            itemHighlight.SetParent(value);
        }
    } 

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField]
    List<ItemData> items;
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Transform canvasTransform;

    [SerializeField]
    ItemHighlight itemHighlight;

    private void Awake()
    {
        itemHighlight = GetComponent<ItemHighlight>();
    }

    // Update is called once per frame
    private void Update()
    {   
        ItemIconDrag();
        
        // get item on mouse
        if(Input.GetKeyDown(KeyCode.Space))
        {   
            if(selectedItem == null)
            {
                CreateRandomItem();
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            InsertRandomItem();
        }

        if(selectedItemGrid == null) 
        { 
            itemHighlight.Show(false);
            return; 
        }
        
        HandleHighlight();
        
        if(Input.GetMouseButtonDown(0))
        {   
            LeftMouseButtonPress();
        }

        // ProcessMouseInput();
    }
    
    // private void ProcessMouseInput()
    // {
    //     if(selectedItem != null)
    //     {   
    //         rectTransform.position = Input.mousePosition;
    //         //selectedItemGrid.position = Input.mousePosition;
    //     }

    //     if(selectedItemGrid == null) { return; }

    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         positionOnGrid = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
    //         if(selectedItem == null)
    //         {
    //             selectedItem = selectedItemGrid.PickUpItem(positionOnGrid);
    //             rectTransform = selectedItem.GetComponent<RectTransform>();
    //         }
    //         else
    //         {
    //             selectedItemGrid.PlaceItem(selectedItem, positionOnGrid.x, positionOnGrid.y);
    //             selectedItem = null;
    //             rectTransform = null;
    //         }
    //     }
    // }

    private void InsertRandomItem()
    {   
        if(selectedItemGrid == null) { return; }
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {   
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if(posOnGrid == null) { return; }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int previousPosition;
    InventoryItem itemToHighlight;
    private void HandleHighlight()
    {   
        Vector2Int positionOnGrid = GetTileGridPosition();
        itemHighlight.SetParent(selectedItemGrid);

        if(previousPosition == positionOnGrid) { return; }
        
        previousPosition = positionOnGrid;
        if(selectedItem == null)
        {   
            // IndexOutRangeException error
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if(itemToHighlight != null)
            {   
                itemHighlight.Show(true);
                itemHighlight.SetSize(itemToHighlight);
                itemHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                itemHighlight.Show(false);
            }
        }
        else
        {
            itemHighlight.Show(selectedItemGrid.BoundaryCheck(
                positionOnGrid.x, 
                positionOnGrid.y, 
                selectedItem.itemData.width,
                selectedItem.itemData.height)
                );
            
            itemHighlight.SetSize(selectedItem);
            itemHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }


    private void LeftMouseButtonPress()
    {   
        Vector2Int tileGridPosition = GetTileGridPosition();

        if(selectedItem == null)
        {   
            PickUpItem(tileGridPosition);
        }
        else{
            PlaceItem(tileGridPosition); 
        }
    }

    public Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if(selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.itemData.height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    public void PlaceItem(Vector2Int tileGridPosition)
    {   
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if(complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    public void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if(selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if(selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }







}
