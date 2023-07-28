using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] public GameObject Package;
    [SerializeField] public GameObject PackageClose;
    [SerializeField] public GameObject PackageButton;

    private void Awake()
    {
        Package.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPackage()
    {
        Package.SetActive(true);
        PackageButton.SetActive(false);
    }

    public void ClosePackage()
    {
        Package.SetActive(false);
        PackageButton.SetActive(true);
    }
}
