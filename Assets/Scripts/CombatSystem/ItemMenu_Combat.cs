using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu_Combat : UIBase
{
    public static ItemMenu_Combat instance;

    [Header("Menu Config")]
    public GameObject itemSlot;
    [SerializeField] private float widthOffset;
    [SerializeField] private float itemMenuWidth;
    [SerializeField] private Transform menuPosition;

    private int itemTypes;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemTypes = ItemStats.instance.bagStats.Count;
    }

    public void Init()
    {
        CreateNewItemSlot();
        //UpdateItemSlotsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemSlotsPosition();
    }

    //private void CreateNewItemSlot(int typeCount)
    private void CreateNewItemSlot()
    {
        foreach(KeyValuePair<int, int> pair in ItemStats.instance.bagStats)
        {

            // Get each item info
            int itemID = pair.Key;
            int itemCount = pair.Value;

            // Init itemSlot
            GameObject newItemSlot = Instantiate(itemSlot, menuPosition);
            newItemSlot.transform.localPosition = Vector3.zero;
            ItemData data = ItemStats.instance.GetItemByID(itemID);
            // Set UI by ID
            newItemSlot.GetComponent<ItemSlot_Combat>().SetItemSlot(data, itemCount);
        }
    }

    public void UpdateItemSlotsPosition()
    {
        
    }

    public void OpenItemMenu()
    {
        // play menu open anim
    }

    public void CloseItemMenu()
    {
        // play menu hide anim
    }
}
