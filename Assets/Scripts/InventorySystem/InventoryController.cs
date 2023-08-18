//All interaction the player will do with all the inventory grids

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{   
    [HideInInspector] private ItemGrid selectedItemGrid;

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

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    ItemHighlight itemHighlight;

    [Header("UI Setting")]
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject storageGrid;

    private void Awake()
    {
        itemHighlight = GetComponent<ItemHighlight>();
    }

    // Update is called once per frame
    private void Update()
    {
        ItemIconDrag();

        if (selectedItemGrid == null)
        {
            itemHighlight.Show(false);
            return;
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InsertRandomItem(); 
        }

    }

    private void InsertRandomItem()
    {   
        if(selectedItemGrid == null) { return; }
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    public void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = SelectedItemGrid.FindSpaceForObject(itemToInsert.itemData);

        if (posOnGrid == null) { return; }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int previousPosition;
    InventoryItem itemToHighlight;
    private void HandleHighlight()
    {   
        Vector2Int positionOnGrid = GetTileGridPosition();

        if(previousPosition == positionOnGrid) { return; }
        
        if(selectedItemGrid.PositionCheck(positionOnGrid.x, positionOnGrid.y) == false) { return; }

        previousPosition = positionOnGrid;
        if(selectedItem == null)
        {   
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
        if(selectedItem != null) { return; }   
        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        InventoryItem newItem = CreateNewInventoryItem(items[selectedItemID]);
        SelectItem(newItem);
    }

    public InventoryItem CreateNewInventoryItem(ItemData itemData)
    {   
        GameObject newItem = Instantiate(itemPrefab);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        inventoryItem.Set(itemData);

        return inventoryItem;
    }

    public void SelectItem(InventoryItem inventoryItem)
    {
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
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

    public void ItemIconDrag()
    {
        if(selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    void ResetPanel()
    {
        //remove the item from the item list
        player.GetSearchPoint().GetComponent<SearchPoint>().items.Clear();
    }

    public void Close()
    {
        storageGrid.SetActive(false);
        player.GetSearchPoint().GetComponent<SearchPoint>().searchButton.SetActive(true);
        player.GetSearchPoint().GetComponent<SearchPoint>().items.Clear();
    }
}
