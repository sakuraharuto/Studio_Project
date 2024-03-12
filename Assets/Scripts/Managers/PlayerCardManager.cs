using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// save player cards and decks
public class PlayerCardManager
{
    public static PlayerCardManager instance = new PlayerCardManager();

    [Header("Card List")]
    public List<string> deck;

    //private void Awake()
    //{
    //    instance = this;
    //}

    public void Init()
    {
        deck = new List<string>
        {
            // Assign cards in deck
            "NormalAttack",
            "NormalAttack",
            "NormalAttack",
            "NormalAttack",
            "NormalAttack",

            "Shield",
            "Shield",
            "Shield",
            "Shield",
            "Shield"
        };
    }
    
}
