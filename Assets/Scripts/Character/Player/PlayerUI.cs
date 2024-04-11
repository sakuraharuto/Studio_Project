using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject PackagePanel;
    public GameObject StatsPanel;

    private InventoryController inventoryController;
    [SerializeField] private ItemGrid packageGrid;
    public bool isInitialized;

    private void Awake()
    {
        isInitialized = false; 
    }

    private void Start()    
    {
        PackagePanel.SetActive(false);
        inventoryController = canvas.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Package Button
    /// </summary>
    public void OpenPackage()
    {
        if (packageGrid != null && isInitialized == false)
        {
            inventoryController.InitialPlayerItems();
            isInitialized = true;
        }
        PackagePanel.SetActive(true);
    }
    public void ClosePackage()
    {
        PackagePanel.SetActive(false);
    }

    /// <summary>
    /// Stats Button
    /// </summary>
    public void OpenStats()
    {
        StatsPanel.SetActive(true);
    }
    public void CloseStats()
    {
        StatsPanel.SetActive(false);
    }

}
