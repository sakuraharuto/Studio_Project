using System.Collections;
using System.Collections.Generic;
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
        originalScale = transform.localScale;
        Debug.Log(originalScale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse hovering");
        transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse click");
        confirmPanel.buildingName = buildingName;
        confirmPanel.gameObject.SetActive(true);
    }


}
