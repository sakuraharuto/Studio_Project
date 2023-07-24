using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchPoint : MonoBehaviour
{   
    // UI
    [SerializeField] private GameObject searchButton;
    [SerializeField] public GameObject searchPointPanel;
    [SerializeField] public GameObject packagePanel;

    // Function
    [SerializeField] InventoryController inventoryController;
    public ItemGrid searchPointItemGrid;

    // public InventoryItem[] items;
    [SerializeField] List<ItemGrid> items;
    public ItemGrid item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        ResourceArrange();
    }

    // public void AddItem(ItemData itemData)
    // {

    // }

    public void ResourceArrange()
    {   
        for(int i=0; i<items.Count; i++)
        {
            item = items[i];
        }
        // inventoryController.InsertItem(item);
        // inventoryItem.Set(items[selectedItemID]);
    }

    public void Search()
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
