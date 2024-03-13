using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

/// <summary>
/// Script of Item Slot in Item Menu
/// </summary>
public class ItemSlot_Combat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // item data
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Image itemIcon;

    [SerializeField] private GameObject highlight;
    private bool isSelected;
    public int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        highlight.SetActive(isSelected);
    }

    public void SetItemSlot(ItemData itemData, int count)
    {   
        // set UI
        itemIcon.sprite = itemData.itemIcon;

        if(itemData.castType == CastType.Default)
        {
            countText.text = count.ToString();
        }
        else
        {
            countText.text = null;
        }

        // attach function script to this item
        System.Type itemType = System.Type.GetType(itemData.itemName);
        Item newItem = (Item)gameObject.AddComponent(itemType);
        newItem.data = itemData;
    }

    public void UpdateItemSlotCount(ItemData itemData)
    {   

        if(itemCount > 0)
        {
            itemCount = ItemStats.instance.bagStats[itemData.itemID];
            countText.text = itemCount.ToString();
        }
        else
        {   
            itemIcon.sprite = null;
            countText.text = null;

            gameObject.GetComponent<Item>().enabled = false;    
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
    }    

}
