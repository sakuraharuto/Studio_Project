using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchPoint : MonoBehaviour
{   
    // UI
    [SerializeField] GameObject searchButton;
    [SerializeField] GameObject packagePanel;
    [SerializeField] GameObject searchPointPanel;

    // Function
    [SerializeField] InventoryController inventoryController;
    [SerializeField] ItemGrid searchPointItemGrid;

    [SerializeField] List<ItemData> items;

    private void Awake()
    {
        searchPointItemGrid.Init();
        ArrangeItems();
    }

    private void Update()
    {
        
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

    // -----UI Interaction-----
    public void OpenPanel()
    {
        searchButton.SetActive(false);
        searchPointPanel.SetActive(true);
        packagePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        searchPointPanel.SetActive(false);
        searchButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            searchButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            searchButton.SetActive(false);
        }
    }
}
