using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    public EnemyData data;
    
    public int enemyIndex;
    public bool isAlive = true;

    public void Init(EnemyData enemyData)
    {
        data = enemyData;

        attributes = new AttributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init(data.maxHP);
        
        HP_Pool = new ValuePool(stats.Get(Statistic.HP));
        HP_Pool.currentValue = data.maxHP;
    }

    public void UpdateStates(EnemyState newState)
    {
        data = newState.data;
        attributes = newState.attributes;
        stats = newState.stats;

        HP_Pool = newState.HP_Pool;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class EnemyState
{
    public int enemyIndex;

    public EnemyData data;

    public bool isAlive;

    public AttributeGroup attributes;
    public StatsGroup stats;     
    public ValuePool HP_Pool;

    public int currentHP;
}
