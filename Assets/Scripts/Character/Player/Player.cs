using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        // Init character data
        attributes = new AttributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init(120);

        HP_Pool = new ValuePool(stats.Get(Statistic.HP));
        HP_Pool.currentValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
