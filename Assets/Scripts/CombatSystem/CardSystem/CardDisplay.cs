using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;

    [Header("Card UI")]
    public Image cardImage;
    //public TMP_Text nameText;
    //public TMP_Text descriptionText;

    //public TMP_Text costText;
    //public TMP_Text attackText;
    //public TMP_Text shieldText;

    private int index;
    public string cardName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This card is: " + card);
    }

    // hovering on card
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On " + cardName);
        //transform.
        
        
        
        //transform.DOScale(1.5f, 0.25f);
        //index = transform.GetSiblingIndex();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
    
}
