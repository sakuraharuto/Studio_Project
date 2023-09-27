using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : UIBase
{   
    // [Header("Manager Config")]
    CombatManager combatManager;
    CardManager cardManager;
    PlayerCardManager playerCardManager;

    // [Header("Cards UI")]
    //private Text deckCount;
    //private Text usedDeckCount;
    //private Text costCount;

    public TMP_Text deckCount;
    public TMP_Text usedDeckCount;
    public TMP_Text costCount;

    // [Header("Player HUD")]
    //character HUD
    //private Text hp;
    //private Text shield;
    public TMP_Text hp;
    public TMP_Text shield;

    [Header("Cards UI")]
    //private List<CardObject> cardDeck;
    public List<Card> cardDeck;

    [SerializeField]
    Transform leftPos;


    public void Awake()
    {
        //deckCount = transform.Find("Canvas/CardDeck_icon/Count").GetComponent<Text>();
        //usedDeckCount = transform.Find("Canvas/UsedCardsDeck_icon/Count").GetComponent<Text>();
        //costCount = transform.Find("Canvas/Cost_icon/Count").GetComponent<Text>();

        //deckCount = transform.Find("Canvas/CardDeck_icon/Count").GetComponent<TextMeshPro>();
        //usedDeckCount = transform.Find("Canvas/UsedCardsDeck_icon/Count").GetComponent<TextMeshPro>();
        //costCount = transform.Find("Canvas/Cost_icon/Count").GetComponent<TextMeshPro>();

        costCount.text = combatManager.playerUnit.cost.ToString();
        deckCount.text = cardManager.cardDeck.Count.ToString();
        usedDeckCount.text = cardManager.usedDeck.Count.ToString();
    }

    public void Start()
    {
        UpdateCost();
        UpdateCardsDeck();
        UpdateUsedCardsDeck();
    }

    public void UpdateCost()
    {
        costCount.text = combatManager.playerUnit.cost.ToString();
    }

    public void UpdateCardsDeck()
    {
        deckCount.text = cardManager.cardDeck.Count.ToString();
    }

    public void UpdateUsedCardsDeck()
    {
        usedDeckCount.text = cardManager.usedDeck.Count.ToString();
    }

    public void UpdateCurrentHP()
    {
        hp.text = combatManager.playerUnit.currentHP.ToString();
    }

    public void UpdateShield()
    {
        shield.text = combatManager.playerUnit.currentShield.ToString();
    }

    public void CreateCardItem(int count)
    {
        if(count > playerCardManager.deck.Count)
        {
            count = playerCardManager.deck.Count;
        }
        //for(int i = 0; i < count; i++)
        //{
        //    GameObject obj = Instantiate()
        //    obj.GetComponent<RectTransform>().anchorPosition = new Vector2(posX, posY);
        //    var item = obj.AddComponent<CardObject>();
        //    string id = combatCardManager.DrawCard();
        //    // Dictionary<string, string> data =
        //    item.Init(data);
        //    cardDeck.Add(item);
        //}
    }

    public void UpdateCardPosition()
    {
        //float offset = 100f;
        //for(int i = 0; i < cardDeck.Count; i++)
        //{
        //    cardDeck.[i].GetComponent<RectTransform>().DOAnchorPos(leftPos, 0.5f);
        //    leftPos.position.x += offset;
        //}
    }

}
