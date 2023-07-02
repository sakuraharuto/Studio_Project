using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BuildingScript : MonoBehaviour
{
    public UnityEvent<string> onBuildingClicked; // 建筑物点击事件
    public string buildingName; // 建筑物名称或标识符

    private void OnMouseDown()
    {
        // 建筑物被点击时触发事件，并传递建筑物信息
        onBuildingClicked.Invoke(buildingName);
    }

    public void SetBuildingName(string name)
    {
        buildingName = name;
    }

    
}


  

   
