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
        CardManager.instance.usedDeck.Add(cardName);
        Debug.Log(CardManager.instance.usedDeck.Count);
    }

    public virtual void CardFunction()
    {

    }
}
