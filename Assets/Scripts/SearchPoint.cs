using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject SearchButton;
    [SerializeField]
    public GameObject SummaryMenu;
    public bool isSearched;
    
    public GameObject[] items;

    // test for text
    public TextMeshProUGUI Text;
    private string itemList;


    // Start is called before the first frame update
    void Start()
    {
        isSearched = false;
        
    }

    public void Search()
    {
        Debug.Log("Searching");

        isSearched = true;
        SearchButton.SetActive(false);
        SummaryMenu.SetActive(true);

        GetNames();

        Text.GetComponent<TextMeshProUGUI>().text = itemList;

        // wait for items
    }

    public void ClosePanel()
    {
        Debug.Log("Got it!");

        SummaryMenu.SetActive(false);
    }

    private void GetNames()
    {
        for(int i = 0; i < items.Length; i++)
        {
            itemList += items[i].name + "\n";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player can search");

        if (other.CompareTag("Player"))
        { 
            if (!isSearched)
            {
                Debug.Log("Show Search Button");
                SearchButton.SetActive(true);
            }
        }
    }
}
