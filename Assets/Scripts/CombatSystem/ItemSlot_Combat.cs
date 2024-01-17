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
    private int id;
    public TMP_Text countText;

    // Start is called before the first frame update
    void Start()
    {   
        countText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {   
        countText.text = ItemStats.instance.bagStats[id].ToString();
    }

    public void SetItemSlot(ItemData itemData, int count)
    {
        // set UI
        id = itemData.itemID;
        gameObject.GetComponent<Image>().sprite = itemData.itemIcon;
        countText.text = count.ToString();

        // add function script
        //gameObject.AddComponent(System.Type.GetType(itemData.itemName));
    }
    
}
