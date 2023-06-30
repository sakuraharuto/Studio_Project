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
        Debug.Log("Pointer Enter");
        //Debug.Log(itemGrid.GetTileGridPosition(Input.mousePosition));
        inverntoryController.selectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        inverntoryController.selectedItemGrid = null;
    }

}
