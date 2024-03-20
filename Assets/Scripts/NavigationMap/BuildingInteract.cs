using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public string buildingName;
    private Vector3 originalScale;

    [SerializeField]private ConfirmPanel confirmPanel;

    // Start is called before the first frame update
    void Start()
    {
        //buildingName = gameObject.name;
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        confirmPanel.buildingName = buildingName;
        confirmPanel.gameObject.SetActive(true);
    }


}
