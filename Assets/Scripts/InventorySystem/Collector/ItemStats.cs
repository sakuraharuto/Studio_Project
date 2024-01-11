using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public static ItemStats instance;

    // use list to store items type
    public List<int> itemTypes = new List<int>();
    // use dictionary to store items count
    [SerializeField] public Dictionary<int, int> bagStats;

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
