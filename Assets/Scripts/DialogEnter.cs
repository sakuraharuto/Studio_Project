using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogEnter : MonoBehaviour
{
    public string buildingName;
    public string sceneName;// 建筑物名称或标识符
    public GameObject loadPanel;
    
    public void SetBuildingName(string building,string scene)
    {
        buildingName = building;
        sceneName = scene; 
    }

    public void ConfirmEnter()
    {
        loadPanel = GameObject.Find("LoadPanel");
        if (loadPanel)
        {
            loadPanel.GetComponent<changeSceneAfterAnimation>().currentDialog = this;
            loadPanel.GetComponent<Animator>().Play("LoadPanelIn");
        }
        else {
            SceneManager.LoadScene(sceneName);
        }
        
    }

    public void LoadScene() {
        SceneManager.LoadScene(sceneName);

    }

    public void CancelEnter()
    {
        Destroy(gameObject);
    }
}