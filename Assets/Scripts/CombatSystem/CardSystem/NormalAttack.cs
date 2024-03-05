using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class NormalAttack : Card
{
    void Start()
    {   
        cardName = data.name;
    }

    public override void CardSpecialEffect()
    {
        // output dmg to enemy
        CombatManager.instance.enemy.GetComponent<Unit>().TakeDamage(data.damage);
        CombatManager.instance.UpdateMonsterInCombat();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //base.OnPointerDown(eventData);

        if (CanUse())
        {
            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();

            CardSpecialEffect();
        }

    }
}
