using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject SearchButton;
    [SerializeField]
    public GameObject SummaryMenu;
    public bool isSearched;
    
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        isSearched = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search()
    {
        Debug.Log("Searching");

        isSearched = true;
        SearchButton.SetActive(false);
        SummaryMenu.SetActive(true);

        // wait for items
    }

    public void ClosePanel()
    {
        Debug.Log("Got it!");

        SummaryMenu.SetActive(false);
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
