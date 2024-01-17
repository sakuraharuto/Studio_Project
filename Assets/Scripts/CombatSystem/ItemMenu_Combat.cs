using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu_Combat : UIBase
{
    public static ItemMenu_Combat instance;

    [Header("Menu Config")]
    public GameObject itemSlot;
    [SerializeField] private float slotWidth = 60f;       // width of each slot
    [SerializeField] private float widthOffset = 10f;     // blank area
    [SerializeField] private float itemMenuWidth;
    [SerializeField] private Transform menuPosition;

    private RectTransform menuRect;

    public List<GameObject> menuList;

    private int itemsCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemsCount = ItemStats.instance.bagStats.Count;

        menuRect= GetComponent<RectTransform>();
    }

    public void Init()
    {
        CreateNewItemSlot();
        //UpdateItemSlotsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateItemSlotsPosition();
    }

    private void CreateNewItemSlot()
    {   
        foreach(KeyValuePair<int, int> pair in ItemStats.instance.bagStats)
        {

            // Get each item info
            int itemID = pair.Key;
            int itemCount = pair.Value;

            // Init itemSlot
            GameObject newItemSlot = Instantiate(itemSlot, menuPosition);
            newItemSlot.transform.SetParent(gameObject.transform);
            newItemSlot.transform.localPosition = Vector3.zero;
            ItemData data = ItemStats.instance.GetItemByID(itemID);
            // Set UI by ID
            newItemSlot.GetComponent<ItemSlot_Combat>().SetItemSlot(data, itemCount);
            menuList.Add(newItemSlot);
        }
    }

    public void UpdateItemSlotsPosition()
    {   
        // set menu background size
        //itemMenuWidth = menuList.Count * ( slotWidth + widthOffset );
        //menuRect.sizeDelta = new Vector2( 10f + itemMenuWidth, 80 );

        // slot position
        for(int i = 0; i < menuList.Count; i++)
        {
            Vector3 posOffset = new Vector3(i * (slotWidth + widthOffset), 0, 0);
            menuList[i].transform.localPosition += posOffset;
        }
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
