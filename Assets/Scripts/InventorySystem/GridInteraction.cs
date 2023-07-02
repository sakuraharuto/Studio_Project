using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    InventoryController inverntoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inverntoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventDatd)
    {
        inverntoryController.SelectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inverntoryController.SelectedItemGrid = null;
    }

}
