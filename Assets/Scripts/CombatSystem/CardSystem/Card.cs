using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// card item base class
public abstract class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [HideInInspector]
    public CardData data;

    [Header("Card Config")]
    //[HideInInspector]
    public Image cardImage;
    //[HideInInspector]
    public string cardName;

    //public Image cardImage;
    //public string cardName;
    //public TMP_Text descriptionText;

    //public TMP_Text costText;
    //public TMP_Text attackText;
    //public TMP_Text shieldText;

    public virtual void InitialData()
    {

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("On " + cardName);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(CanUse())
        {   
            // add this card into used deck
            //CardManager.instance.usedDeck.Add(cardName);
            Debug.Log(CardManager.instance.usedDeck.Count);

            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();

        }
        
    }

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
            CombatUI.instance.UpdateCost();

            UIManager.instance.GetUI<CombatUI>("CombatUI").RemoveCard(this);

            return true;
        }
    }

    public virtual void CardFunction()
    {

    }
}
