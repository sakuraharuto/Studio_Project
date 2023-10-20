using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay : Card
{
    public void InitialDisplay(CardData data)  
    {   
        // set visual
        cardImage.sprite = data.image;
        cardName = data.cardName;

        // add function script
        System.Type cardType = System.Type.GetType(data.cardName);
        Card newCard = (Card)gameObject.AddComponent(cardType);
        // initial functon
        newCard.data = data;
        newCard.InitialData();
    }
    
}
