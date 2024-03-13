using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

// card script base class
public abstract class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [HideInInspector]
    public CardData data;

    [Header("Card Config")]
    public string cardName;
    public Image cardImage;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(0.5f, 0.25f);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(0.4f, 0.25f);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //if(CanUse())
        //{   
        //    // update player's UI
        //    CombatUI.instance.UpdateCardsDeck();
        //    CombatUI.instance.UpdateUsedCardsDeck();
        //}
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
