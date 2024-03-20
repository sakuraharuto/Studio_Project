using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
   
    private GameObject CurrentPanel;
    private bool isPanelOpen = false;
    public List<GameObject> panels; // 
    private Dictionary<string, GameObject> panelDictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (var panel in panels)
        {
            panelDictionary.Add(panel.name, panel);
        }
    }

    public void ShowPopup(string panelName)
    {
        if (!isPanelOpen)
        {
            if (panelDictionary.TryGetValue(panelName, out GameObject panel))
            {
                panel.SetActive(true);
                isPanelOpen = true; // 更新状态变量
            }
            else
            {
                Debug.LogWarning("No panel found with the name " + panelName);
            }
        }
        else
        {
            Debug.LogWarning("Another panel is already open. Close it first before opening another.");
        }
    }

    public void HidePopup(string panelName)
    {
        if (panelDictionary.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(false);
            isPanelOpen = false; // 更新状态变量
        }
        else
        {
            Debug.LogWarning("No panel found with the name " + panelName);
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in panelDictionary.Values)
        {
            panel.SetActive(false);
        }
        isPanelOpen = false;
    }

    public void GoTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  
    }
}
