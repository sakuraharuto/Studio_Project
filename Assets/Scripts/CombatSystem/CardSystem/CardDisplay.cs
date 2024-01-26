using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay : Card
{
    public void InitialDisplay(CardData data)  
    {   
        // Initial Card Image
        cardImage.sprite = data.image;
        cardName = data.cardName;
    }
    
}
