using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// save player cards and decks
public class PlayerCardManager
{
    public static PlayerCardManager Instance = new PlayerCardManager();

    [Header("Card List")]
    public List<string> deck;

    public void Init()
    {
        //Debug.Log("Player has cards");
        deck = new List<string>();

        //Assign cards in deck
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
    
}
