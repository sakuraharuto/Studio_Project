using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject SearchButton;
    [SerializeField]
    public GameObject SearchPointGrid;
    [SerializeField]
    public GameObject PackageGrid;

    // public bool isOpen;
    public GameObject[] items;

    // test for text
    // public TextMeshProUGUI Text;
    // private string itemList;

    // Start is called before the first frame update
    void Start()
    {
        // isOpen = false;
    }

    public void Search()
    {
        Debug.Log("Look what we found here!");

        // isOpen = true;
        SearchButton.SetActive(false);
        SearchPointGrid.SetActive(true);
        PackageGrid.SetActive(true);

        // GetNames();
        // Text.GetComponent<TextMeshProUGUI>().text = itemList;
    }

    public void ClosePanel()
    {
        SearchPointGrid.SetActive(false);
    }

    // private void GetNames()
    // {
    //     for(int i = 0; i < items.Length; i++)
    //     {
    //         itemList += items[i].name + "\n";
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            SearchButton.SetActive(true);
        }
    }
}
