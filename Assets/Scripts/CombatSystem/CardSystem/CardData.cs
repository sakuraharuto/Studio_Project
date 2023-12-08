using UnityEngine;

// save all card data
[CreateAssetMenu(fileName = "New Card", menuName = "Combat System/Card/CardObject")]
public class CardData : ScriptableObject
{
    [Header("Card Config")]
    public string cardName;
    public string description;
    public bool isAOE;

    public Sprite image;

    public int manaCost;
    public int damage;
    public int shield;
}
