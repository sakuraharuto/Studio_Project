using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject PackagePanel;
    public GameObject StatsPanel;

    private InventoryController inventoryController;

    private void Awake()
    {
        PackagePanel.SetActive(false);
    }

    private void Start()
    {
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
        PackagePanel.SetActive(true);
        inventoryController.InitialPlayerItems();
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
