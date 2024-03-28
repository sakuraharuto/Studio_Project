using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class NormalAttack : Card
{
    public override void OnPointerDown(PointerEventData eventData)
    {   
        base.OnPointerDown(eventData);
    }

    public override void CardSpecialEffect()
    {   
        // output dmg to enemy
        CombatManager.instance.enemyUnit.TakeDamage(data.damage);
    }
}
