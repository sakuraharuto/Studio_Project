using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shield : Card
{
    public override void OnPointerDown(PointerEventData eventData)
    { 
        base.OnPointerDown(eventData);
    }

    public override void CardSpecialEffect()
    {
        CombatManager.instance.playerUnit.currentShield += data.shield;
    }
}
