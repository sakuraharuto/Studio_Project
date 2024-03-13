using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System.Linq;

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

    [Header("Cards UI")]
    public GameObject cardPrefab;   // show as hand card

    [SerializeField] private RectTransform cardPoint;

    // store all cards and data
    public CardData[] allCards;

    // list of hand cards to manage position
    public List<CardDisplay> cardList;

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
        usedDeckCount.text = cardList.Count.ToString();
        costCount.text = CombatManager.instance.playerUnit.cost.ToString();
    }

    public void Start()
    {
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
            obj.GetComponent<RectTransform>().anchoredPosition = cardPoint.GetComponent<RectTransform>().position;
            // Get card data from scriptableObject
            CardData data = DrawCard();

            // wait for update
            // Integrate CardDisplay into Card child scripts
            #region
            // Attach card image
            CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
            cardDisplay.InitialDisplay(data);
            // add to hand cards list to manage position
            cardList.Add(obj.GetComponent<CardDisplay>());
            #endregion

            // add and initial function
            System.Type cardType = System.Type.GetType(data.cardName);
            Card newCard = (Card)obj.AddComponent(cardType);
            newCard.data = data;
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
        float offset = 400f / cardList.Count;
        
        Vector2 startPos = new Vector2(-cardList.Count / 2f * offset + offset / 2f, 0);

        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x += offset;
        }
    }

    // delete card after use
    public void RemoveCard(Card card)
    {   
        // disable card function
        card.enabled = false;
        // access the UI component
        CardDisplay panel = card.GetComponent<CardDisplay>();
        // add this card into used-card deck
        CardManager.instance.usedDeck.Add(card.cardName);
        // update used-card deck count ui
        usedDeckCount.text = CardManager.instance.usedDeck.Count.ToString();
        // remove from handcards list
        cardList.Remove(panel);
        // update handcards pos
        UpdateCardPosition();

        Destroy(panel.gameObject);
    }

    public void DropHandCards()
    {   
        for(int i = cardList.Count-1; i >= 0; i--)
        {
            RemoveCard(cardList[i]);
        }
    }

}
