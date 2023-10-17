using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : UIBase
{
    [Header("Manager Config")]
    public static CombatUI instance;
    public Transform canvasTF;


    [Header("Icons")]
    //private Text deckCount;
    //private Text usedDeckCount;
    //private Text costCount;

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
    [SerializeField] private float offset;

    // store all cards and data
    public Card[] allCards;
    public List<GameObject> cardList;

    // public CardDisplay[] allCards;
    //public Card card;

    public void Awake()
    {   
        instance = this;

        canvasTF = GameObject.Find("Canvas").transform;

        // load all cards
        allCards = Resources.LoadAll<Card>("Cards");
        Debug.Log("cards: " + allCards.Length);

    }

    public void Start()
    {
        //UpdateCost();
        //UpdateCardsDeck();
        //UpdateUsedCardsDeck();
    }

    public void UpdateCost()
    {
        costCount.text = CombatManager.instance.playerUnit.cost.ToString();
        Debug.Log("Player has: " + costCount.text);
    }

    public void UpdateCardsDeck()
    {
        deckCount.text = CardManager.Instance.cardDeck.Count.ToString();
    }

    public void UpdateUsedCardsDeck()
    {
        usedDeckCount.text = CardManager.Instance.usedDeck.Count.ToString();
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
        if(count > PlayerCardManager.Instance.deck.Count)
        {
            count = PlayerCardManager.Instance.deck.Count;
        }

        for (int i = 0; i < count; i++)
        {
            Card card = DrawCard();

            GameObject obj = Instantiate(cardPrefab, canvasTF);
            obj.GetComponent<CardDisplay>().cardImage.sprite = card.image;

            cardList.Add(obj);  // update hand card deck
        }
    }

    public Card DrawCard()
    {
        // get the card on the top of deck
        string name = CardManager.Instance.DrawCard();

        // find the card based on name
        foreach(var Card in allCards)
        {
            //Debug.Log(name);
            if(Card.GetName() == name)
            {
                //Debug.Log("The card is " + name);
                return Card;
            }
        }

        return null;
    }

    // arrange positions of hand cards
    public void UpdateCardPosition()
    {
        Debug.Log("Moving");
        
        float offset = 480f / cardList.Count;
        Vector2 handPos = new Vector2(-handLeftPoint.anchoredPosition.x, handLeftPoint.anchoredPosition.y);

        for (int i = 0; i < cardList.Count; i++)
        {   
            //Vector2 handPos = new Vector2()
            cardList[i].GetComponent<RectTransform>().anchoredPosition = handPos;
            handPos.x += offset;
            
        }
    }

}
