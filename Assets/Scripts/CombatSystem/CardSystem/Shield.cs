using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shield : Card
{
    public override void OnPointerDown(PointerEventData eventData)
    { 
        if (CanUse())
        {
            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();

            CombatManager.instance.playerUnit.currentShield += data.shield;
            CombatUI.instance.UpdateShield();
        }
    }
    public override void CardSpecialEffect()
    {

    }
}
