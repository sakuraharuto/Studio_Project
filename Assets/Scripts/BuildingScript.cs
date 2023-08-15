using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
[System.Serializable]
public class BuildingClickedEvent : UnityEvent<BuildingEventData> { }

[System.Serializable]
public struct BuildingEventData
{
    public string buildingName;
    public string sceneName;

    public BuildingEventData(string building, string scene)
    {
        buildingName = building;
        sceneName = scene;
    }
}
public class BuildingScript : MonoBehaviour
{
    public BuildingClickedEvent onBuildingClicked; // 建筑物点击事件
    public string buildingName; // 建筑物名称或标识符
    public string sceneName; // 建筑物索引或其他变量
 

    private void OnMouseDown()
    {
        BuildingEventData eventData = new BuildingEventData(buildingName, sceneName);
        onBuildingClicked.Invoke(eventData);
    }




    private Vector3 originalScale;
    private bool isMouseOver = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        transform.localScale = originalScale * 1.1f;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        transform.localScale = originalScale;
    }

    private void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0)) // Adjust the condition if needed
        {
            // Perform an action when the object is clicked while the mouse is over it
            Debug.Log("Object Clicked!");
        }
    }

    
}


  

   
