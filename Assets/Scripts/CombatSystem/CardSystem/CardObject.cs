using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class CardObject
{
    public Dictionary<string, string> data;

    [Header("Card Config")]
    public string id;
    public string cardName;

    public bool isUsed;

    public int cost;
    public int damage;
    public int shield;
    
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    public string GetID() { return id; }

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
