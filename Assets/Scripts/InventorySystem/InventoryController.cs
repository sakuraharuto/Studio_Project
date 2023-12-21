using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

//All interaction the player will do with all the inventory grids
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

    private void Awake()
    {
        itemHighlight = GetComponent<ItemHighlight>();
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
        if(storageGrid.activeSelf == false )
        {
            selectedItem = null;
            selectedItemGrid = null;
        }

        //drag item's icon
        if(selectedItem != null)
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

    private void NullSelectedItem()
    {
        selectedItem = null;
        rectTransform = null;
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
        //click when item selected
        else
        {
            PlaceItemInput();
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

    private void PlaceItemInput()
    {
        if(selectedItemGrid.BoundaryCheck(positionOnGrid.x, positionOnGrid.y,
                selectedItem.itemData.width, selectedItem.itemData.height) == false)
        {
            return;
        }

        if(selectedItemGrid.OverlapCheck(positionOnGrid.x, positionOnGrid.y, 
            selectedItem.itemData.width, selectedItem.itemData.height, 
            ref overlapItem) == false)
        {
            overlapItem = null;
            return;
        }

        if(overlapItem != null) 
        {
            selectedItemGrid.CleanGridReference(overlapItem);
        }

        selectedItemGrid.PlaceItem(selectedItem, positionOnGrid.x, positionOnGrid.y);
        NullSelectedItem();

        if(overlapItem != null)
        {
            selectedItem = overlapItem;
            rectTransform = selectedItem.GetComponent<RectTransform>();
            overlapItem = null;
        }
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

    public void Close()
    {
        storageGrid.SetActive(!storageGrid.activeInHierarchy);
    }
}
