using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OneTime Object", menuName = "Inventory System/Items/OneTime")]
public class OneTimeObject : ItemObject
{
    public void Awake()
    {
        castType = CastType.OneTime;
    }
}
