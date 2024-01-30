using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class NormalAttack : Card
{
    void Start()
    {   
        cardName = data.name;
    }

    //public override void InitialData()
    //{

    //}

    //public override void CardFunction()
    //{
    //    // output dmg to enemy
    //    CombatManager.instance.enemyUnit.currentHP -= data.damage;
    //}

    public override void OnPointerDown(PointerEventData eventData)
    {
        //base.OnPointerDown(eventData);
        //CardFunction();

        if (CanUse())
        {
            // update player's UI
            CombatUI.instance.UpdateCardsDeck();
            CombatUI.instance.UpdateUsedCardsDeck();
        }
    }
}
