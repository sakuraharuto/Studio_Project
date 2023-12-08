using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Type for shown On Player Grid
public enum EquipmentSlot
{ 
    Weapon,
    Armor,
    Medicine
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory System/GridSystem/ItemData")]
public abstract class ItemData : ScriptableObject
{
    // icon config
    public Sprite itemIcon;
    public int width = 1 ;
    public int height = 1;

}
