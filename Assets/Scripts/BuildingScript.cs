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

   
    
}


  

   
