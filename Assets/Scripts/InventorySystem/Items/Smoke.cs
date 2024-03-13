using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Smoke : Item
{
    // Start is called before the first frame update
    void Start()
    {
        itemName = data.itemName;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {   
        base.OnPointerDown(eventData);
    }

    public override void ItemEffect()
    {
        ItemMenu_Combat.instance.gameObject.SetActive(false);

        CombatManager.instance.Flee();
    }
}
