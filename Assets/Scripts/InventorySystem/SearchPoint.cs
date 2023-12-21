using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] PlayerMovement player;

    static int t = 1;

    private void Start()
    {
        containerPanel.SetActive(false);
    }

    private void Update()
    {
        if (searchButton.activeSelf ) 
        {
            if (Input.GetKeyUp(KeyCode.E)) { Open(); }
        }
    }

    public void AddItems(ItemData itemData)
    {
        Vector2Int? positionToAdd = containerGrid.FindSpaceForObject(itemData);

        if(positionToAdd == null ) { return; }

        InventoryItem newItem = inventoryController.CreateNewInventoryItem(itemData);
        containerGrid.PlaceItem(newItem, positionToAdd.Value.x, positionToAdd.Value.y);
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
        searchButton.SetActive(false);
        
        foreach(ItemData newItem in items)
        {
            AddItems(newItem);
        }
        containerPanel.SetActive(!containerPanel.activeInHierarchy);
        //containerPanel.SetActive(true);
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
