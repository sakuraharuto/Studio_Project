using UnityEngine;
using UnityEngine.EventSystems;

public class NormalAttack : Card
{
    private int cost;
    private int dmg;

    public override void InitialData()
    {   
        cardName = data.name;
        dmg = data.damage;
        cost = data.manaCost;
    }

    public override void CardFunction()
    {
        CombatManager.instance.TakeDamage(dmg);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        CardFunction();

        //CardManager.instance.usedDeck.Add(cardName);
        
        //Debug.Log(CardManager.instance.usedDeck.Count);
    }

}
