using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    public static CardManager Instance = new CardManager();

    public List<string> cardDeck;
    public List<string> usedDeck;

    public void Init()
    {
        cardDeck = new List<string>();
        usedDeck = new List<string>();

        List<string> tempDeck = new List<string>(); 

        //shuffle
    }

    public bool HasCards()
    {
        return cardDeck.Count > 0;  
    }

    public string DrawCard()
    {
        string id = cardDeck[cardDeck.Count - 1];

        cardDeck.RemoveAt(cardDeck.Count - 1);

        return id;
    }
}
