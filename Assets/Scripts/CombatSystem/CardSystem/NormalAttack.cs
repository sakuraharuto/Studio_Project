using UnityEngine;
using UnityEngine.EventSystems;

public class NormalAttack : MonoBehaviour, IPointerDownHandler
{
    // public Card card;
    public CardDisplay cardDisplay;

    private int dmg = 0;

    // Start is called before the first frame update
    void Start()
    {   
        cardDisplay = GetComponent<CardDisplay>();
        dmg = cardDisplay.card.damage;
    }

    // Update is called once per frame
    void Update()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("The damage is " + dmg);
    }
}
