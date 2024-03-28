using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public abstract class Item : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    protected ItemData data;

    [Header("Item Config")]
    public string itemName;
    public CastType castType;

    //double click setting;
    public bool doubleClicked;
    private float lastClick;
    private float clickGap = 0.5f;

    //public bool canUse = false;

    public void Init(ItemData itemData)
    {
        data = itemData;
    }

    void Update()
    {
        lastClick += Time.deltaTime;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if ((Time.time - lastClick) < clickGap)
        {
            doubleClicked = true;

            if(CanUse())
            {
                //state = TurnState.INITIAL;
                if ( castType == CastType.Default )
                {
                    if (ItemStats.instance.bagStats[data.itemID] == 1)
                    {
                        ItemStats.instance.bagStats.Remove(data.itemID);
                    }
                    else if(ItemStats.instance.bagStats[data.itemID] > 1)
                    {
                        ItemStats.instance.bagStats[data.itemID]--;
                    }

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

                ItemEffect();
            }
        }
        else
        {
            doubleClicked = false;
        }
        lastClick = Time.time;
    }

    public virtual bool CanUse()
    {
        int cost = data.cost;
        
        // check cost is enough to use item
        if(cost > CombatManager.instance.playerUnit.cost)
        {
            Debug.Log("Cost is not enought");
            //canUse = false;

            return false;
        }
        else
        {   
            //canUse = true;
            // update player's cost and its UI
            CombatManager.instance.playerUnit.cost -= cost;
            CombatUI.instance.UpdateCost();

            return true;
        }
    }

    public abstract void ItemEffect();
    
}
