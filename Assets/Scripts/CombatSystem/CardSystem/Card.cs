using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Card Config")]
    public string cardName;
    public string description;

    public Sprite image;

    public int manaCost;
    public int damage;
    public int shield;

    public string GetName(){ return cardName; }
    
    public int GetManaCost() { return manaCost;}
    public int GetDamage() { return damage;}
    public int GetShield() {  return shield;}
}
