using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    PlayerCardManager playerCardManager;

    public List<string> cardDeck;
    public List<string> usedDeck;

    CardObject card;

    public void Init()
    {
        cardDeck = new List<string>();
        usedDeck = new List<string>();

        List<string> tempDeck = new List<string>();
        tempDeck.AddRange(playerCardManager.deck);
        
        //shuffle player's deck for each combat
        while(tempDeck.Count > 0)
        {
            int tempPos = Random.Range(0, tempDeck.Count);

            cardDeck.Add(tempDeck[tempPos]);

            tempDeck.RemoveAt(tempPos);
        }

        Debug.Log(cardDeck.Count);
    }

    // check for shuffle
    public bool HasCards()
    {
        return cardDeck.Count > 0;  
    }

    public string GetCardID()
    {
        return card.GetID();
    }

    public void InitCards()
    {   
        for(int i = 0; i < 5; i++)
        {
            cardDeck.Add("1001");
            cardDeck.Add("1002");
        }
        cardDeck.Add("1003");
        cardDeck.Add("1004");
    }

    // shuffle, used deck ==> deck
    public void Shuffle()
    {

    }

    // return card at the last position in the list
    public string DrawCard()
    {   
        string id = cardDeck[cardDeck.Count - 1];

        cardDeck.RemoveAt(cardDeck.Count - 1);

        return id;
    }

}
