using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class SearchPoint : MonoBehaviour
{
    // UI
    [Header("UI Setting")]
    [SerializeField] public GameObject searchButton;
    [SerializeField] public GameObject packagePanel;
    [SerializeField] public GameObject containerPanel;

    [SerializeField] int gridSizeWidth;
    [SerializeField] int gridSizeHeight;
     
    // Function
    [Header("Function Setting")]
    [SerializeField] InventoryController inventoryController;
    [SerializeField] ItemGrid containerGrid;

    [Header("Item List")]
    public List<ItemData> items;
    public List<ItemData> allItems;
    public List<InventoryItem> inventoryItemsList;

    [SerializeField] PlayerMovement player;

    static int t = 1;

    private void Start()
    {
        containerPanel.SetActive(false);
    }

    private void Update()
    {
        if (searchButton.activeSelf) 
        {
            if (Input.GetKeyUp(KeyCode.E)) { Open(); }
        }
    }

    private void AddItems()
    {
        for(int i = 0; i < items.Count; i++)
        {
            InventoryItem newItem = inventoryController.CreateNewInventoryItem(items[i]);
            inventoryItemsList.Add(newItem);

            Vector2Int? posOnGrid = containerGrid.FindSpaceForObject(newItem.itemData);

            if (posOnGrid == null) { return; }

            containerGrid.PlaceItem(newItem, posOnGrid.Value.x, posOnGrid.Value.y);
        }
    }

    private void RemoveItems()
    {
        if(inventoryItemsList == null) { return; }
        
        for(int i = 2; i < containerGrid.transform.childCount; i++)
        {
            Destroy(containerGrid.transform.GetChild(i).gameObject);
        }

        containerGrid.EmptyGrid();
        items.Clear();
    }

    private void RefreshResource()
    {   
        int newItemCount = Random.Range(1, allItems.Count);

        for(int i=0; i< newItemCount; i++)
        {
            int randomItem = Random.Range(0, allItems.Count);
            items.Add(allItems[randomItem]);
        }
    }

    // -----UI Interaction-----
    public void Open()
    {
        searchButton.SetActive(false);
        containerPanel.SetActive(!containerPanel.activeInHierarchy);
        packagePanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected.");
            searchButton.SetActive(true);

            if (t == 1)
            {
                return;
            }
            else
            {
                AddItems();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // clean and refresh items in container 
            RemoveItems();
            RefreshResource();

            searchButton.SetActive(false);
            t++;
        }
    }
}
