using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemStats))]
public class DictionaryInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemStats itemStats = (ItemStats)target;

        if(GUILayout.Button("Show Backpack Contents"))
        {
            foreach(KeyValuePair<int, int> pair in itemStats.bagStats)
            {
                Debug.Log("Item_" + pair.Key + " x" + pair.Value);
            }
        }
    }
}
