using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    [SerializeField] private RectTransform handLeftPoint;

    // store all cards and data
    public Card[] allCards;
    // public List<GameObject> cardList;
    public List<CardDisplay> cardList;

    public void Awake()
    {   
        instance = this;

        canvasTF = GameObject.Find("Canvas").transform;

        // load all cards
        allCards = Resources.LoadAll<Card>("Cards");

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
        deckCount.text = "10";
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

        for (int i = 0; i < count; i++)
        {
            //Card card = DrawCard();

            //GameObject obj = Instantiate(cardPrefab, canvasTF);

            StartCoroutine(SetCardData());
            //obj.GetComponent<CardDisplay>().cardImage.sprite = card.image;
            //obj.GetComponent<CardDisplay>().cardName = card.name;
            //CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
            //cardDisplay.card = card;

            // attach component to card
            // obj.AddComponent(Type.GetType(card.name));

            // cardList.Add(obj);  // update hand card
            // cardList.Add(item);
        }
    }

    IEnumerator SetCardData()
    {
        Card card = DrawCard();

        GameObject obj = Instantiate(cardPrefab, canvasTF);

        obj.GetComponent<CardDisplay>().cardImage.sprite = card.image;
        obj.GetComponent<CardDisplay>().cardName = card.name;

        CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
        cardDisplay.card = card;
        yield return null;

        // attach component to card
        obj.AddComponent(Type.GetType(card.name));

    }

    public Card DrawCard()
    {
        // get the card on the top of deck
        string name = CardManager.instance.DrawCard();

        // find the card based on name
        foreach(var Card in allCards)
        {
            if(Card.GetName() == name)
            {
                return Card;
            }
        }

        return null;
    }

    // arrange positions of hand cards
    public void UpdateCardPosition()
    {   
        float offset = 480f / cardList.Count;
        Vector2 handPos = new Vector2(handLeftPoint.anchoredPosition.x, handLeftPoint.anchoredPosition.y);

        for (int i = 0; i < cardList.Count; i++)
        {   
            //Vector2 handPos = new Vector2()
            //cardList[i].GetComponent<RectTransform>().anchoredPosition = handPos;
            handPos.x += offset;
        }
    }

}
