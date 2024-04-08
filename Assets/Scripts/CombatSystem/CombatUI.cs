using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CombatUI : UIBase
{
    [Header("Manager Config")]
    public static CombatUI instance;
    public Transform canvasTF;

    [Header("Icons")]
    [SerializeField] public TMP_Text deckCount;
    [SerializeField] public TMP_Text usedDeckCount;
    [SerializeField] public TMP_Text costCount;

    [Header("Player HUD")]
    public TMP_Text hp;
    public TMP_Text shield;
    public Slider healthBarSlider;

    [Header("Cards UI")]
    public GameObject cardPrefab;   // show as hand card

    [SerializeField] private RectTransform cardPoint;

    // store all cards and data
    public CardData[] allCards;

    // list of hand cards to manage position
    public List<Card> inHandCards;


    public void Awake()
    {
        instance = this;

        // load all cards
        allCards = Resources.LoadAll<CardData>("Cards");

        StartCoroutine(AssignUIText());
    }

    IEnumerator AssignUIText()
    {
        yield return null;

        //assign player ui data
        deckCount.text = PlayerCardManager.instance.deck.Count.ToString();
        usedDeckCount.text = inHandCards.Count.ToString();
        costCount.text = CombatManager.instance.playerUnit.cost.ToString();
    }

    public void Start()
    {
        healthBarSlider = CombatManager.instance.player.transform.GetChild(0).GetComponent<Slider>();

        UpdateHealthBar();
        UpdateCardsDeck();
        UpdateUsedCardsDeck();
    }

    public void UpdateCost()
    {   
        costCount.text = CombatManager.instance.playerUnit.cost.ToString();
    }

    public void UpdateCardsDeck()
    {
        deckCount.text = CardManager.instance.cardDeck.Count.ToString();
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

    public void UpdateHealthBar()
    {
        healthBarSlider.value = CombatManager.instance.playerUnit.currentHP;
    }


    public void UpdateShield()
    {
        shield.text = CombatManager.instance.playerUnit.currentShield.ToString();
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
            // instantiate a card object
            GameObject obj = Instantiate(cardPrefab, canvasTF);
            obj.GetComponent<RectTransform>().anchoredPosition = cardPoint.position;
            // Get card data from scriptableObject
            CardData data = DrawCard();
            // Init card data and function
            Card card = obj.AddComponent(System.Type.GetType(data.cardName)) as Card;
            card.InitData(data);
            // Add to hand cards list
            inHandCards.Add(card);
        }
    }

    // return data from ScriptablObject
    public CardData DrawCard()
    {
        // get the card on the top of deck
        string name = CardManager.instance.DrawCard();

        // find the card based on name
        foreach (var CardData in allCards)
        {
            if (CardData.cardName == name)
            {
                return CardData;
            }
        }

        return null;
    }

    // arrange positions of hand cards
    public void UpdateCardPosition()
    {
        float offset = 400f / inHandCards.Count;
        
        Vector2 startPos = new Vector2(-inHandCards.Count / 2f * offset + offset / 2f, 0);

        for (int i = 0; i < inHandCards.Count; i++)
        {
            inHandCards[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x += offset;
        }
    }

    // delete card after use
    public void RemoveCard(Card card)
    {   
        // disable card function
        card.enabled = false;
        // access the UI component
        Card panel = card.GetComponent<Card>();
        // add this card into used-card deck
        CardManager.instance.usedDeck.Add(card.cardName);
        // update used-card deck count ui
        usedDeckCount.text = CardManager.instance.usedDeck.Count.ToString();
        // remove from handcards list
        inHandCards.Remove(panel);
        // update handcards pos
        UpdateCardPosition();
        // delete card obj
        Destroy(panel.gameObject);
    }

    public void DropHandCards()
    {   
        for(int i = inHandCards.Count-1; i >= 0; i--)
        {
            RemoveCard(inHandCards[i]);
        }
    }

}
