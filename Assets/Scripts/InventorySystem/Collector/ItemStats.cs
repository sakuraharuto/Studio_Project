using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    // use dictionary to store items count
    public Dictionary<int, int> bagStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        bagStats = new Dictionary<int, int>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
