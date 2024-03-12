//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager all interaction of cards and deck
public class CardManager
{   
    public static CardManager instance = new CardManager();

    public List<string> cardDeck = new List<string>();
    public List<string> usedDeck = new List<string>();

    //private void Awake()
    //{
    //    instance = this;
    //}

    public void Init()
    {   
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

    public bool HasCards()
    {
        return cardDeck.Count > 0;  
    }

    // shuffle, used deck ==> deck
    public void Shuffle()
    {
        Debug.Log("Shuffling");

        cardDeck.AddRange(usedDeck);
        usedDeck.Clear();

        for(int i = 0; i < cardDeck.Count; i++)
        {
            int tempPos = Random.Range(i, cardDeck.Count - 1);
            string tempCard = cardDeck[tempPos];
            cardDeck[tempPos] = cardDeck[i];
            cardDeck[i] = tempCard;
        }

        CombatUI.instance.UpdateCardsDeck();
        CombatUI.instance.UpdateUsedCardsDeck();
    }

    // return card at the last position in the list
    public string DrawCard()
    {   
        string name = cardDeck[cardDeck.Count - 1];
        cardDeck.RemoveAt(cardDeck.Count - 1);

        CombatUI.instance.UpdateCardsDeck();

        return name;
    }
}
