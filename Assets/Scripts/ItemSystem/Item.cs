using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public ItemData data;

    [Header("Item Config")]
    public string itemName;
    public CastType castType;

    //double click setting;
    public bool doubleClicked;
    private float lastClick;
    private float clickGap = 0.5f;

    public bool canUse = false;

    void Update()
    {
        lastClick += Time.deltaTime;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (lastClick < clickGap)
        {
            doubleClicked = true;

            if(CanUse())
            {
                //state = TurnState.INITIAL;
                if ( castType == CastType.Default )
                {
                    //Once use, count--
                    Debug.Log(castType);

                    ItemStats.instance.bagStats[data.itemID]--;
                    gameObject.GetComponent<ItemSlot_Combat>().UpdateItemSlotCount(data);
                }

                if ( data.castType == CastType.OneTime)
                {   
                    //Once use, delete from backpack
                    Debug.Log(castType);
                }
            
                if (data.castType == CastType.Reusable)
                {   
                    //Once use, in CD
                    Debug.Log(castType);
                }

                ItemMenu_Combat.instance.gameObject.SetActive(false);
            }

        }
        else
        {
            lastClick = 0f;
        }

    }

    public virtual bool CanUse()
    {
        int cost = data.cost;
        
        // check cost is enough to use item
        if(cost > CombatManager.instance.playerUnit.cost)
        {
            Debug.Log("Cost is not enought");
            canUse = false;

            return false;
        }
        else
        {   
            canUse = true;
            // update player's cost and its UI
            CombatManager.instance.playerUnit.cost -= cost;
            CombatUI.instance.UpdateCost();

            return true;
        }
    }

    public virtual void ItemEffect()
    {

    }
}
