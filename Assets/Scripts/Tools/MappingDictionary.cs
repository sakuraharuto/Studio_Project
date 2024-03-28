using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingDictionary : MonoBehaviour
{
    [SerializeField] string thisObjName;

    [SerializeField] NewDictionary newDictionary;

    public Dictionary<string, List<int>> thisDictionary;

    private void Awake()
    {
        thisDictionary = newDictionary.ToDictionary();
    }
}

[Serializable]
public class NewDictionary
{
    [SerializeField] NewPair[] thisPairs;

    public Dictionary<string, List<int>> ToDictionary()
    {
        Dictionary<string, List<int>> newDictionary = new Dictionary<string, List<int>>();
        {
            foreach(var element in thisPairs)
            {
                newDictionary.Add(element.mapName, element.enemyID);    
            }

            return newDictionary;
        }
    }

}

[Serializable]
public class NewPair
{
    public string mapName;
    public List<int> enemyID;
}

