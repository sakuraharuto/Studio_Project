using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

// card script base class
public abstract class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [HideInInspector]
    public CardData data;

    [Header("Card Info")]
    public string cardName;

    public virtual void Start()
    {
        // Initial Card Image
        cardName = data.cardName;

        transform.Find("card_image").GetComponent<Image>().sprite = data.image;
        transform.Find("card_bg").GetComponent<Image>().sprite = data.bg;
        transform.Find("card_name").GetComponent<TMP_Text>().text = data.cardName;
        //transform.Find("card_description").GetComponent<TMP_Text>().text = data.description;
        transform.Find("card_cost").GetComponent<TMP_Text>().text = data.manaCost.ToString();

    }

    public void InitData(CardData data)
    {
        this.data = data;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.25f);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public abstract void CardSpecialEffect();

    public virtual bool CanUse()
    {
        int cost = data.manaCost;

        if(cost > CombatManager.instance.playerUnit.cost)
        {   
            Debug.Log("Cost is not enought");
            return false;
        }
        else
        {   
            // update player's cost and its UI
            CombatManager.instance.playerUnit.cost -= cost; 
            // remove card from hand, then add it into used-cards deck
            CombatUI.instance.RemoveCard(this);
            CombatUI.instance.UpdateCost();

            return true;
        }
    }
}
