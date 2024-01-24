using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] public GameObject PackageGrid;
    [SerializeField] public GameObject PackageClose;
    [SerializeField] public GameObject PackageButton;

    private void Start()
    {
        //PackageClose.SetActive(false);
        PackageGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPackage()
    {
        PackageGrid.SetActive(true);
        //PackageClose.SetActive(true);
        PackageButton.SetActive(false);
    }

    public void ClosePackage()
    {
        PackageGrid.SetActive(false);
        //PackageClose.SetActive(false);
        PackageButton.SetActive(true);
    }

}
