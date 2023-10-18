using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// save player cards and decks
public class PlayerCardManager
{
    public static PlayerCardManager instance = new PlayerCardManager();

    [Header("Card List")]
    public List<string> deck;

    public void Init()
    {
        //Debug.Log("Player has cards");
        deck = new List<string>
        {
            // Assign cards in deck
            "NormalAttack",
            "NormalAttack",
            "NormalAttack",
            "NormalAttack",

            //"Shield",
            //"Shield",
            //"Shield",
            //"Shield",

            //"Special",
            //"Item"

            // test deck
            //"Attack",
            //"Attack",
            //"Attack",
            //"Attack",
            
            //"Attack",
            //"Attack",
            //"Attack",
            //"Attack",
            
            //"Attack",
            //"Attack"
        };
    }
    
}
