using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] public GameObject PackagePanel;
    [SerializeField] public GameObject StatsPanel;

    private void Start()
    {
        PackagePanel.SetActive(false);
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
