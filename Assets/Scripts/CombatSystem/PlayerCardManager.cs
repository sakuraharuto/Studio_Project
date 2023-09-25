using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public List<string> deck;

    public void Init()
    {
        Debug.Log("Player has cards");
        deck = new List<string>();

        deck.Add("1001");
        deck.Add("1001");
        deck.Add("1001");
        deck.Add("1001");

        deck.Add("1002");
        deck.Add("1002");
        deck.Add("1002");
        deck.Add("1002");

        deck.Add("1003");
        deck.Add("1004");
    }
}
