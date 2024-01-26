using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class NormalAttack : Card
{   

    public override void InitialData()
    {

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
