using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shield : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = data.name;
    }

    //public override void InitialData()
    //{
    //    
    //}

    //public override void CardFunction()
    //{   
    //    CombatManager.instance.playerUnit.currentShield += data.shield;
    //    CombatUI.instance.UpdateShield();
    //}

    public override void OnPointerDown(PointerEventData eventData)
    {
        //base.OnPointerDown(eventData);

        if (CanUse())
        {
            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();

            CombatManager.instance.playerUnit.currentShield += data.shield;
            CombatUI.instance.UpdateShield();
        }

        //CombatManager.instance.playerUnit.currentShield += data.shield;
        //CombatUI.instance.UpdateShield();

        //CardFunction();
    }
}
