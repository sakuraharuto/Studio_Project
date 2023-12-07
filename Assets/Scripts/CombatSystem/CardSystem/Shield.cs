using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shield : Card
{
    public override void InitialData()
    {
        cardName = data.name;
    }

    public override void CardFunction()
    {
        Debug.Log("Add " + data.shield + " shield");
        CombatManager.instance.playerUnit.currentShield += data.shield;
        CombatUI.instance.UpdateShield();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        CardFunction();
    }
}
