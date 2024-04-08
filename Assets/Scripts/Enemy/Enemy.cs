using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    protected EnemyData data;
    
    public int enemyID;
    public bool isAlive = true;

    public void Init(EnemyData enemyData)
    {
        data = enemyData;

        enemyID = data.enemyID;
        combatSprite = data.combatSprite;
        characterName = data.name;

        attributes = new AttributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init();

        HP_Pool = new ValuePool(stats.Get(Statistic.HP));
        HP_Pool.currentValue = data.maxHP;
    }

    public void UpdateStates(EnemyState newState)
    {
        this.isAlive = newState.isAlive;
        this.HP_Pool.currentValue = newState.currentHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!isAlive)
        //{
        //    Destroy(gameObject);
        //}
    }
}

[System.Serializable]
public class EnemyState
{
    public int enemyID;
    public Vector3 pos;
    public bool isAlive;
    public int currentHP;
}
