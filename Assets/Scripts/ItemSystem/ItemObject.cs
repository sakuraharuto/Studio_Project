using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ItemType
//{ 

//}

public enum CastType
{
    Default,
    OneTime,
    Reusable
}

public abstract class ItemObject : ScriptableObject
{
    // Config
    public CastType castType;

    public int itemID;
    public string itemName;
    public Sprite itemIcon;

    [TextArea(15,20)]
    public string description;

    //public ItemType itemType;
}
