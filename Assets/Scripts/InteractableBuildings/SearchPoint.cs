using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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

    [SerializeField] PlayerMovement player;

    private void Start()
    {
        containerPanel.SetActive(false);
        RefreshResource();
    }

    private void Update()
    {
        if(searchButton.activeSelf) 
        {
            if (Input.GetKeyUp(KeyCode.E)) { Open(); }
        }

    }

    private void AddItems()
    {   
        for(int i = 0; i < this.items.Count; i++)
        {
            InventoryItem newItem = inventoryController.CreateNewInventoryItem(this.items[i]);

            Vector2Int? posOnGrid = containerGrid.FindSpaceForObject(newItem.itemData);

            if (posOnGrid == null) { return; }

            containerGrid.PlaceItem(newItem, posOnGrid.Value.x, posOnGrid.Value.y);
        }
    }

    private void RemoveItems()
    {
        for(int i = 2; i < containerGrid.transform.childCount; i++)
        {
            if (containerGrid.transform.GetChild(i).gameObject.name == "Highlighter")
            {
                break;
            }
            else
            {
                Destroy(containerGrid.transform.GetChild(i).gameObject);
            }
        }

        containerGrid.EmptyGrid();
    }

    private void RefreshResource()
    {
        this.items.Clear();

        int newItemCount = Random.Range(1, allItems.Count);

        for(int i=0; i < newItemCount; i++)
        {
            int randomItem = Random.Range(0, allItems.Count);
            this.items.Add(allItems[randomItem]);
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
        if(other.CompareTag("Player"))
        {
            searchButton.SetActive(true);
            
            AddItems();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            searchButton.SetActive(false);

            if (containerGrid.isEmpty() == true)
            {
                RefreshResource();
            }

            if (containerGrid.isEmpty() == false)
            {
                RemoveItems();
            }
    
        }
    }
}
