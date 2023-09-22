using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : UIBase
{
    private Text cardsCount;
    private Text usedCardsCount;
    private Text costCount;

    //character HUD
    private Text hp;
    private Text shield;


    private void Awake()
    {   
        cardsCount = transform.Find("Canvas/CardDeck_icon/Count").GetComponent<Text>();
        usedCardsCount = transform.Find("Canvas/UsedCardsDeck_icon/Count").GetComponent<Text>();
        costCount = transform.Find("Canvas/Cost_icon/Count").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateCost();
        UpdateCardsDeck();
        UpdateUsedCardsDeck();
    }

    public void UpdateCost()
    {
        //costCount = 
    }

    public void UpdateCardsDeck()
    {
        cardsCount.text = CardManager.Instance.cardDeck.Count.ToString();
    }

    public void UpdateUsedCardsDeck()
    {
        usedCardsCount.text = CardManager.Instance.usedDeck.Count.ToString();
    }

    public void UpdateCurrentHP()
    {
        
    }

    public void UpdateShield()
    {

    }
}
