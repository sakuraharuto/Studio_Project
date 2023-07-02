using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogEnter : MonoBehaviour
{
    private string buildingName; // 建筑物名称或标识符

    public void SetBuildingName(string name)
    {
        buildingName = name;
    }

    public void ConfirmEnter()
    {
        // 根据建筑物信息加载对应的建筑物场景
        SceneManager.LoadScene(buildingName);
    }

    public void CancelEnter()
    {
        Destroy(gameObject);
    }
}