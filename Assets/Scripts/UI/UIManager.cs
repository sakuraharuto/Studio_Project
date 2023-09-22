using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvas;

    private List<UIBase> uiList;

    private void Awake()
    {
        Instance = this;
        canvas = GameObject.Find("Canvas").transform;

        uiList = new List<UIBase>();
    }

    public UIBase OpenUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null)
        {
            GameObject uiObject = Instantiate(Resources.Load("UI/" + uiName), canvas) as GameObject;

            uiObject.name = uiName;

            ui = uiObject.AddComponent<T>();

            uiList.Add(ui);
        }
        else
        {
            ui.Open();
        }

        return ui;
    }
    
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            ui.Close();
        }    
    }

    public void CloseAllUI()
    {
        for (int i = uiList.Count-1; i >=0; i--)
        {
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();
    }

    public void DestroyUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null) 
        {
            uiList.Remove(ui); 
            Destroy(ui.gameObject);
        }
    }

    public UIBase Find(string uiName)
    {
        for(int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }
        return null;
    }
}