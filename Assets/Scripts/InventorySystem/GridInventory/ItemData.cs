using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{ 
    Weapon,
    Armor,
    Item,
    Medicine
}

public enum CastType
{
    Default,
    OneTime,
    Reusable
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory System/GridSystem/ItemData")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public CastType castType;

    // icon config
    public Sprite itemIcon;
    public int width = 1 ;
    public int height = 1;

    [TextArea(15, 20)]
    public string description;
}
