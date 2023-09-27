using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// save player cards and decks
public class PlayerCardManager : MonoBehaviour
{
    CardManager cardManager;

    [Header("Card List")]
    public List<string> deck;

    public void Init()
    {
        Debug.Log("Player has cards");
        deck = new List<string>();

        deck.Add("Attack");
        deck.Add("Attack");
        deck.Add("Attack");
        deck.Add("Attack");

        deck.Add("Shield");
        deck.Add("Shield");
        deck.Add("Shield");
        deck.Add("Shield");

        deck.Add("Special");
        deck.Add("Item");
    }
    
    public void DrawCard()
    {
        deck.Add(cardManager.DrawCard());
    }
}
