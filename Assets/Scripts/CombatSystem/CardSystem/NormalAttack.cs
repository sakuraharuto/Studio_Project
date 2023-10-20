using UnityEngine;
using UnityEngine.EventSystems;

public class NormalAttack : Card
{   
    private int dmg;

    public override void InitialData()
    {   
        cardName = data.name;
        dmg = data.damage;
    }

    public override void CardFunction()
    {
        CombatManager.instance.TakeDamage(dmg);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        CardFunction();
    }

}
