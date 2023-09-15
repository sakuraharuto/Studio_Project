using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : ScriptableObject
{
    [Header("Card Config")]
    public string cardName;

    public bool isUsed;

    public int cost;
    public int damage;
    public int shield;

    //Player player;

    public int GetCost() { return cost; }
    public int GetDamage() 
    {
        //if (player.weapon != null)
        //{
        //    return damage += weapon.powerUp; 
        //}
        //else
        //{
            return damage;
        //}
    }
    public int GetShield()
    {
        //if (player.equip != null)
        //{
        //    return shield += equip.powerUp; 
        //}
        //else
        //{
        return shield;
        //}
    }

}
