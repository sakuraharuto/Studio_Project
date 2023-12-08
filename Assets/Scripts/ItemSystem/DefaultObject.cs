using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Save generic data Only
//  Implement functions in indivdual class
//  e.g:
//  FlashGenerade class:
//    int stunSecond;
//    void ClickToStun(){}

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        castType = CastType.Default;
    }
}
