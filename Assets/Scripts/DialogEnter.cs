using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogEnter : MonoBehaviour
{
    public string buildingName;
    public string sceneName;// 建筑物名称或标识符
    
    
    public void SetBuildingName(string building,string scene)
    {
        buildingName = building;
        sceneName = scene; 
    }

    public void ConfirmEnter()
    {
        // 根据建筑物信息加载对应的建筑物场景
        SceneManager.LoadScene(sceneName);
    }

    public void CancelEnter()
    {
        Destroy(gameObject);
    }
}