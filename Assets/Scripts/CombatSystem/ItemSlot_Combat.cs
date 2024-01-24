using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Script of Item Slot in Item Menu
/// </summary>
public class ItemSlot_Combat : MonoBehaviour
{
    // item data
    [SerializeField] private int id;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Image itemIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemSlot(ItemData itemData, int count)
    {   
        // set UI
        this.id = itemData.itemID;
        this.countText.text = count.ToString();
        this.itemIcon.sprite = itemData.itemIcon;

        // add function script
    }
    
}
