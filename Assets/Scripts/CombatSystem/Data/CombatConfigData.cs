using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CombatConfigData
{
    private List<Dictionary<string, string>> data;

    public CombatConfigData(string str)
    {
        data = new List<Dictionary<string, string>>();

        // Split next line
        string[] line = str.Split('\n');
        // first line --- type
        string[] title = line[0].Trim().Split('\t');
        // start from 2nd line
        for(int i = 1; i < line.Length; i++)
        {   
            Dictionary<string, string> configList = new Dictionary<string, string>();

            string[] temp = line[i].Trim().Split('\t');

            for(int j = 0; j < temp.Length; j++)
            {
                configList.Add(title[j], temp[j]);
            }

            data.Add(configList);

        }
    }

    public List<Dictionary<string, string>> GetLine()
    {
        return data;
    }

    public Dictionary<string, string> GetOneById(string id)
    {
        for(int i = 0; i < data.Count; i++)
        {
            Dictionary<string, string> dataID = data[i];
            if (dataID["id"] == id)
            {
                return dataID;
            }
        }
        return null;
    }
}
