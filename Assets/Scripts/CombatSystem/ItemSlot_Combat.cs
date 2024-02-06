using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script of Item Slot in Item Menu
/// </summary>
public class ItemSlot_Combat : MonoBehaviour, IPointerClickHandler
{
    // item data
    [SerializeField] private int id;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Image itemIcon;

    [SerializeField] private GameObject highlight;
    private bool isSelected;

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
        id = itemData.itemID;
        countText.text = count.ToString();
        itemIcon.sprite = itemData.itemIcon;

        // attach function script to this item
        System.Type itemType = System.Type.GetType(itemData.itemName);
        Item newItem = (Item)gameObject.AddComponent(itemType);
        newItem.data = itemData;
    }

    public void UpdateItemSlotCount(ItemData itemData)
    {
        countText.text = ItemStats.instance.bagStats[itemData.itemID].ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        // left-click to use item
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        // right-click to deSelect
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        isSelected = true;
        
    }

    public void OnRightClick()
    {
        isSelected = false;
    }
}
