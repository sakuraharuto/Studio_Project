using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [HideInInspector]
    public ItemData data;

    [Header("Item Config")]
    public string itemName;
    public CastType castType;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
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
    }

}
