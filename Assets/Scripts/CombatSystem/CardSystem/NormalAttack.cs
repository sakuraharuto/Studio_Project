using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class NormalAttack : Card
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (CanUse())
        {
            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();

            CardSpecialEffect();
        }
    }

    public override void CardSpecialEffect()
    {
        // output dmg to enemy
        CombatManager.instance.enemyUnit.TakeDamage(data.damage);
    }
}
