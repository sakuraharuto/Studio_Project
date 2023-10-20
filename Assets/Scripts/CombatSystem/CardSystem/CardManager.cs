//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager all interaction of cards and deck
public class CardManager
{   
    public static CardManager instance = new CardManager();

    public List<string> cardDeck;  //
    public List<string> usedDeck;   

    // CardObject card;

    public void Init()
    {   
        cardDeck = new List<string>();
        usedDeck = new List<string>();

        //load cards into deck and shuffle
        List<string> tempDeck = new List<string>();
        tempDeck.AddRange(PlayerCardManager.instance.deck);
        while (tempDeck.Count > 0)
        {
            int tempPos = Random.Range(0, tempDeck.Count);

            cardDeck.Add(tempDeck[tempPos]);

            tempDeck.RemoveAt(tempPos);
        }
    }

    // check for shuffle
    public bool HasCards()
    {
        return cardDeck.Count > 0;  
    }

    // shuffle, used deck ==> deck
    public void Shuffle()
    {
        Debug.Log("Shuffle cards deck.");
        for(int i = 0; i < cardDeck.Count-1; i++)
        {
            int tempPos = Random.Range(1, cardDeck.Count);
            cardDeck[tempPos] = usedDeck[i];
            usedDeck.RemoveAt(i);
        }
    }

    // return card at the last position in the list
    public string DrawCard()
    {
        string name = cardDeck[cardDeck.Count - 1];
        cardDeck.RemoveAt(cardDeck.Count - 1);
        //Debug.Log("Deck has "+ cardDeck.Count + " cards left.");

        return name;
    }

}
