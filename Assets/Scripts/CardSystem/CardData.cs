using UnityEngine;

// save all card data
[CreateAssetMenu(fileName = "New Card", menuName = "Combat System/Card/CardObject")]
public class CardData : ScriptableObject
{
    [Header("Card Config")]
    public int cardID;
    public string cardName;
    public string description;

    public Sprite image;
    public Sprite bg;

    public int manaCost;
    public int damage;
    public int shield;
}
