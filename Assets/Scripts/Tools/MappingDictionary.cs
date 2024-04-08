using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingDictionary : MonoBehaviour
{
    [SerializeField] string thisObjName;

    [SerializeField] NewDictionary newDictionary;

    //public Dictionary<string, List<int>> thisDictionary;
    public Dictionary<string, int[]> thisDictionary;

    private void Awake()
    {
        thisDictionary = newDictionary.ToDictionary();
    }
}

[Serializable]
public class NewDictionary
{
    [SerializeField] NewPair[] thisPairs;

    //public Dictionary<string, List<int>> ToDictionary()
    public Dictionary<string, int[]> ToDictionary()
    {
        //Dictionary<string, List<int>> newDictionary = new Dictionary<string, List<int>>();
        Dictionary<string, int[]> newDictionary = new Dictionary<string, int[]>();
        
        foreach(var element in thisPairs)
        {
            newDictionary.Add(element.key, element.valueList);    
        }

        return newDictionary;
    }

}

[Serializable]
public class NewPair
{
    public string key;
    //public List<int> valueList;
    public int[] valueList;
}

