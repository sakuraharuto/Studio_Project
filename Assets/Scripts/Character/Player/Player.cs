using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        // Init character data
        state = SpecialStates.Normal;

        attributes = new AttributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init(120);

        HP_Pool = new ValuePool(stats.Get(Statistic.HP));
        HP_Pool.currentValue = 100;

        if (GameManager.instance.postCombatPlayerStats != null)
        {
            UpdatePostCombatStats(GameManager.instance.postCombatPlayerStats);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePostCombatStats(CombatUnit unit)
    {
        Debug.Log("Update player stats");

        state = unit.state;

        HP_Pool.currentValue = unit.currentHP;
    }
}
