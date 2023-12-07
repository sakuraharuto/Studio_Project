using UnityEngine;
using UnityEngine.EventSystems;

public class NormalAttack : Card
{   

    public override void InitialData()
    {
        cardName = data.name;
    }

    public override void CardFunction()
    {
        //Debug.Log(data.damage);

        // output dmg to enemy
        CombatManager.instance.enemyUnit.currentHP -= data.damage;
        //Debug.Log("enemy hp: " + CombatManager.instance.enemyUnit.currentHP);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        CardFunction();
    }

}
