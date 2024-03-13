using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay : Card
{
    public void InitialDisplay(CardData data)  
    {   
        // Initial Card Image
        cardName = data.cardName;
        cardImage.sprite = data.image;
    }

    public override void CardSpecialEffect()
    {

    }
}
