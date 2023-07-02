using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogEnterPrefab; // 对话框的Prefab引用
    public Canvas canvas; // Canvas的引用

    private void Start()
    {
        BuildingScript[] buildings = FindObjectsOfType<BuildingScript>();
        foreach (BuildingScript building in buildings)
        {
            building.onBuildingClicked.AddListener(CreateDialogEnter);
        }
    }

    private void CreateDialogEnter(BuildingEventData eventData)
    {
        // 动态创建对话框，并设置其父对象为Canvas
        GameObject dialogEnterObj = Instantiate(dialogEnterPrefab, canvas.transform);
        DialogEnter dialogEnterScript = dialogEnterObj.GetComponent<DialogEnter>();

        // 设置建筑物信息
        dialogEnterScript.SetBuildingName(eventData.buildingName,eventData.sceneName);
    }
}