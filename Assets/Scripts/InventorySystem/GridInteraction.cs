// Making Grid interactable 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    InventoryController inverntoryController;
    ItemGrid itemGrid;

    private void Start()
    {
        inverntoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inverntoryController.SelectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inverntoryController.SelectedItemGrid = null;
    }

}
