using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPoint : MonoBehaviour
{
    // UI
    [Header("UI Setting")]
    [SerializeField] public GameObject searchButton;
    [SerializeField] public GameObject packagePanel;
    [SerializeField] public GameObject searchPointGridPanel;

    // Function
    [Header("Function Setting")]
    [SerializeField] InventoryController inventoryController;
    [SerializeField] ItemGrid searchPointItemGrid;

    [Header("Item List")]
    public List<ItemData> items;
    public List<ItemData> allItems;

    [SerializeField] PlayerMovement player;

    static int t = 1;

    private void Start()
    {   
        searchPointItemGrid.Init();
    }

    public void ArrangeItems()
    {   
        for(int i=0; i<items.Count; i++)
        {
            ItemData itemToPlace = items[i]; 

            Vector2Int? positionToPlace = searchPointItemGrid.FindSpaceForObject(itemToPlace);
            if (positionToPlace == null) { return; }
            InventoryItem newItem = inventoryController.CreateNewInventoryItem(itemToPlace);
            searchPointItemGrid.PlaceItem(newItem, positionToPlace.Value.x, positionToPlace.Value.y);
        }
    }

    public void RefreshResource()
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
        items = player.GetSearchPoint().GetComponent<SearchPoint>().items;
        for (int i = 0; i < items.Count; i++)
        {
            inventoryController.CreateNewInventoryItem(items[i]);
        }
        ArrangeItems();

        searchButton.SetActive(false);
        searchPointGridPanel.SetActive(true);
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
                RefreshResource();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            items.Clear();

            searchButton.SetActive(false);

            t++;
        }
    }
}
