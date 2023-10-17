using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public string data;

    //public List<Dictionary<string, string>> listdata;

    public void Init(string data)
    {
        this.data = data;
    }
}
