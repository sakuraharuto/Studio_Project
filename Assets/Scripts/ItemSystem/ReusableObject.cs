using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reusable Object", menuName = "Inventory System/Items/Reusable")]
public class ReusableObject : ItemObject
{
    public void Awake()
    {
        castType = CastType.Reusable;
    }
}
