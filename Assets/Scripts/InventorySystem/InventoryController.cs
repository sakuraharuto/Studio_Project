using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.UIElements;
using UnityEngine;

//All interaction the player will do with all the inventory grids
public class InventoryController : MonoBehaviour
{   
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid {
        get => selectedItemGrid;
        set {
            selectedItemGrid = value;
            itemHighlight.SetParent(value);
        }
    }

    Vector2Int positionOnGrid;
    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;    //for drage icon

    //items var
    [SerializeField] List<ItemData> items;  //contain all items for test
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    [SerializeField] ItemHighlight itemHighlight;
    InventoryItem itemToHighlight;

    [Header("UI Setting")]
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject storageGrid;
    [SerializeField] ItemGrid packageGrid;

    private void Awake()
    {
        itemHighlight = GetComponent<ItemHighlight>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {   
        ProcessMouseInput();

        HandleHighlight();

        // test only
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InsertRandomItem();
        }

    }

    public void InitialPlayerItems()
    {
        if (ItemStats.instance.bagStats != null)
        {
            foreach (var element in ItemStats.instance.bagStats)
            {
                ItemData newItemData = ItemStats.instance.GetItemByID(element.Key);
                for(int i = 1; i <= element.Value; i++)
                {
                    InventoryItem newItem = CreateNewInventoryItem(newItemData);

                    Vector2Int? posOnGrid = packageGrid.FindSpaceForObject(newItem.itemData);
                    if (posOnGrid == null) { return; }
                    packageGrid.PlaceItem(newItem, posOnGrid.Value.x, posOnGrid.Value.y);
                }
            }
        }
    }

    public void InsertRandomItem()
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
    private void HandleHighlight()
    {
        //hide highlight when no item selected
        if (selectedItemGrid == null)
        {
            itemHighlight.Show(false);
            return;
        }

        Vector2Int positionOnGrid = GetTileGridPosition();
        if (positionOnGrid == previousPosition) { return; }

        if (selectedItemGrid.PositionCheck(positionOnGrid.x, positionOnGrid.y) == false) { return; }

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
        InventoryItem newInventoryItem = newItem.GetComponent<InventoryItem>();

        RectTransform newItemRectTransform = newItem.GetComponent<RectTransform>();
        newItemRectTransform.SetParent(canvasTransform);

        newInventoryItem.previousParent = newInventoryItem.transform.parent;
        newInventoryItem.currentParent = newInventoryItem.transform.parent;

        newInventoryItem.Set(itemData);

        return newInventoryItem;
    }

    public void SelectItem(InventoryItem inventoryItem)
    {
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
    }

    public void ProcessMouseInput()
    {
        // set both to null when the player hides panels
        if (storageGrid.activeSelf == false)
        {
            selectedItem = null;
            selectedItemGrid = null;
        }

        //drag item's icon
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }

        if(selectedItemGrid == null) { return; }

        if(Input.GetMouseButtonDown(0))
        {   
            if(selectedItemGrid != null)
            {
                ItemGridInput();
            }
        }
    }

    private void ItemGridInput()
    {
        positionOnGrid = GetTileGridPosition();

        //click when no item selected
        if (selectedItem == null)
        {
            selectedItem = selectedItemGrid.PickUpItem(positionOnGrid);
            if (selectedItem != null)
            {
                SelectItem(selectedItem);
            }
        }
        //click when has item selected
        else
        {
            PlaceItemInput();
        }
    }

    public Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;
        if (selectedItem != null)
        { 
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.itemData.height - 1) * ItemGrid.tileSizeHeight / 2;
        }
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItemInput()
    {
        // return if the position is NOT on grid
        if (selectedItemGrid.BoundaryCheck(positionOnGrid.x, positionOnGrid.y,
                selectedItem.itemData.width, selectedItem.itemData.height) == false)
        {
            selectedItem = null;
            rectTransform = null;
            return;
        }

        // return if there is NO item overlapped
        if (selectedItemGrid.OverlapCheck(positionOnGrid.x, positionOnGrid.y,
            selectedItem.itemData.width, selectedItem.itemData.height,
            ref overlapItem) == false)
        {
            overlapItem = null;
            return;
        }

        // clean the position of overlapped item on grid 
        if (overlapItem != null)
        {
            selectedItemGrid.CleanGridReference(overlapItem);
        }

        // Place selectedItem on the grid
        selectedItemGrid.PlaceItem(selectedItem, positionOnGrid.x, positionOnGrid.y);

        Debug.Log(positionOnGrid.x + " " + positionOnGrid.y);

        selectedItem.currentParent = selectedItem.transform.parent;
        
        // Update Item Count
        if(CheckItemMoved(selectedItem))
        {
            if (selectedItemGrid.name == "Package_Grid")
            {
                CollectItem(selectedItem.id);
                selectedItem.previousParent = selectedItemGrid.transform;
                player.sp.items.Remove(selectedItem.itemData);
            }
            if (selectedItemGrid.name == "Container_Grid")
            {
                DropItem(selectedItem.id);
                selectedItem.previousParent = selectedItemGrid.transform;
                if (overlapItem == null) { player.sp.items.Add(selectedItem.itemData); }
            }
        }

        selectedItem = null;
        rectTransform = null;

        // Set the overlappedItem as selectedItem
        if (overlapItem != null)
        {
            selectedItem = overlapItem;
            rectTransform = selectedItem.GetComponent<RectTransform>();
            overlapItem = null;
        }
    }

    private bool CheckItemMoved(InventoryItem selectedItem)
    {
        if(selectedItem.currentParent != selectedItem.previousParent)
        {
            return true;
        }

        return false;
    }

    //Update the Count of item in bag
    private void CollectItem(int itemID)
    {
        if(ItemStats.instance.bagStats.ContainsKey(itemID))
        {
            ItemStats.instance.bagStats[itemID]++;
        }
        else
        {
            ItemStats.instance.bagStats.Add(itemID, 1);
        }
    }

    private void DropItem(int itemID)
    {
        if(ItemStats.instance.bagStats[itemID] == 1)
        {
            ItemStats.instance.bagStats.Remove(itemID);
        }
        else
        {
            ItemStats.instance.bagStats[itemID]--;
        }
    }

    //private void DropItem(InventoryItem inventoryItem)
    //{
    //    Debug.Log("Mouse pos is outside grids.");
    //    Destroy(inventoryItem);
    //    selectedItem = null;
    //}

    public void Close()
    {
        storageGrid.SetActive(false);

        player.sp.searchButton.SetActive(true);
    }
}
