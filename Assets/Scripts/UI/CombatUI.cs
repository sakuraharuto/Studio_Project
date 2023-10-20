using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEditor;

public class CombatUI : UIBase
{
    [Header("Manager Config")]
    public static CombatUI instance;
    public Transform canvasTF;

    [Header("Icons")]
    //private TMP_Text deckCount;
    //private TMP_Text usedDeckCount;
    //private TMP_Text costCount;

    public TMP_Text deckCount;
    public TMP_Text usedDeckCount;
    public TMP_Text costCount;

    [Header("Player HUD")]
    //character HUD
    //private Text hp;
    //private Text shield;
    public TMP_Text hp;
    public TMP_Text shield;

    [Header("Cards UI")]
    public GameObject cardPrefab;   // show as hand card

    [SerializeField] private Transform handPoint;

    // store all cards and data
    public CardData[] allCards;

    // list of hand cards to arrange position
    public List<CardDisplay> cardList;

    public void Awake()
    {   
        instance = this;

        // canvasTF = GameObject.Find("Canvas").transform;

        // load all cards
        allCards = Resources.LoadAll<CardData>("Cards");

    }

    public void Start()
    {
        //UpdateCost();
        UpdateCardsDeck();
        //UpdateUsedCardsDeck();
    }

    public void UpdateCost()
    {
        costCount.text = CombatManager.instance.playerUnit.cost.ToString();
        Debug.Log("Player has: " + costCount.text);
    }

    public void UpdateCardsDeck()
    {
        // deckCount.text = CardManager.instance.cardDeck.Count.ToString();
        //deckCount.text = "10";
    }

    public void UpdateUsedCardsDeck()
    {
        usedDeckCount.text = CardManager.instance.usedDeck.Count.ToString();
    }

    public void UpdateCurrentHP()
    {
        hp.text = CombatManager.instance.playerUnit.currentHP.ToString();
        Debug.Log("Player has: " + hp.text);
    }

    public void UpdateShield()
    {
        shield.text = CombatManager.instance.playerUnit.currentShield.ToString();
        Debug.Log("Player has: " + shield.text);
    }

    // count => draw COUNT cards from deck
    public void CreateCardItem(int count)
    {
        if(count > PlayerCardManager.instance.deck.Count)
        {
            count = PlayerCardManager.instance.deck.Count;
        }

        Debug.Log("Draw "+ count + " cards from deck");

        for (int i = 0; i < count; i++)
        {   
            // Access data from scriptableObject
            CardData data = DrawCard();
            // instantiate a card object
            GameObject obj = Instantiate(cardPrefab, handPoint.transform);
            // initial card UI
            CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
            cardDisplay.InitialDisplay(data);
            // add to hand card list
            cardList.Add(obj.GetComponent<CardDisplay>());

        }
    }

    // return data from ScriptablObject
    public CardData DrawCard()
    {
        // get the card on the top of deck
        string name = CardManager.instance.DrawCard();

        // find the card based on name
        foreach(var CardData in allCards)
        {
            if(CardData.cardName == name)
            {
                return CardData;
            }
        }

        return null;
    }

    // arrange positions of hand cards
    public void UpdateCardPosition()
    {   
        float offset = 480f / cardList.Count;
        Vector2 handPos = new Vector2(-handPoint.position.x, handPoint.position.y);

        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].GetComponent<RectTransform>().anchoredPosition = handPos;
            handPos.x += offset;
        }
    }

}
