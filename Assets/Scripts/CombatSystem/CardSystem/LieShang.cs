using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LieShang : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = data.name;
    }

    private void OnEnable()
    {
        CardSpecialEffect();
    }

    public override void CardSpecialEffect()
    {
        CombatManager.instance.player.GetComponent<Unit>().TakeDamage(data.damage);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {

    }
}