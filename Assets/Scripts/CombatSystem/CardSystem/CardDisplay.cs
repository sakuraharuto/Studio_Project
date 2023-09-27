using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public Image cardImage;

    public TMP_Text manaText;
    //public TMP_Text attackText;
    //public TMP_Text shieldText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.cardName;
        descriptionText.text = card.description;

        cardImage.sprite = card.image;

        manaText.text = card.manaCost.ToString();

    }

}
