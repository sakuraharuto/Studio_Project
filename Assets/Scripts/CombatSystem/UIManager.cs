using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Transform canvas;

    private List<UIBase> uiList;

    private void Awake()
    {
        instance = this;

        //canvas = GameObject.Find("Canvas").transform;

        uiList = new List<UIBase>();
    }

    public UIBase OpenUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);

        // Init UI Object
        // Need to Rewrite
        if (ui == null)
        {   
            // Load All UI objects in List
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
    
    // Hide a specific UI
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            ui.Close();
        }    
    }

    // close a specific UI
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if( ui != null )
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    // Close All ui
    public void CloseAllUI()
    {
        for (int i = uiList.Count-1; i >= 0; i--)
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
        UIBase ui = FindObjectOfType<UIBase>();

        return ui;
        //Debug.Log(uiList.Count);
        //for (int i = 0; i < uiList.Count; i++)
        //{
        //    if (uiList[i].name == uiName)
        //    {
        //        return uiList[i];
        //    }
        //}
        //return null;
    }

    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        //Debug.Log(ui);
        if( ui != null )
        {
            return ui.GetComponent<T>();
        }
        return null;
    }

}
