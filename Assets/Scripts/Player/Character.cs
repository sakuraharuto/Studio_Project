using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// character States
public enum SpecialStates
{
    Normal,
    LieShang,
    Stun,
    Pain
}

// Character Stats
public enum Statistic
{ 
    HP,
    Damage,
    Armor,
    Mana,
    HPReg
}

[Serializable]
public class StatsValue
{
    public Statistic statisticType;
    public int value;

    public StatsValue(Statistic statisticType, int value)
    {
        this.statisticType = statisticType;
        this.value = value;
    }
}

[Serializable]
public class StatsGroup
{
    public List<StatsValue> stats;

    public StatsGroup()
    {
        stats = new List<StatsValue>();
    }

    public void Init()
    {
        stats.Add(new StatsValue(Statistic.HP, 10));
        stats.Add(new StatsValue(Statistic.Damage, 0));
        stats.Add(new StatsValue(Statistic.Armor, 0));
        stats.Add(new StatsValue(Statistic.Mana, 10));
        stats.Add(new StatsValue(Statistic.HPReg, 1));
    }

    internal StatsValue Get(Statistic statsToGet)
    {
        return stats[(int)statsToGet];
    }
}

// Character Attributes
public enum Attribute
{
    Strength,
    Agility,
    Intelligence
}

[Serializable]
public class AttributeValue
{ 
    public Attribute attributeType;
    public int value;

    public AttributeValue(Attribute attributeType, int value = 0)
    {
        this.attributeType = attributeType;
        this.value = value;
    }
}

[Serializable]
public class AttributeGroup 
{
    public List<AttributeValue> attributeValues;

    public AttributeGroup()
    {
        attributeValues = new List<AttributeValue>();
    }

    public void Init()
    {
        attributeValues.Add(new AttributeValue(Attribute.Strength));
        attributeValues.Add(new AttributeValue(Attribute.Agility));
        attributeValues.Add(new AttributeValue(Attribute.Intelligence));
    }
}

[Serializable]
public class ValuePool
{ 
    public StatsValue maxValue;
    public int currentValue;

    public ValuePool(StatsValue maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue.value;
    }
}

public class Character : MonoBehaviour
{
    public string characterName;

    public SpecialStates state;
    [SerializeField] AttributeGroup attributes;     //RPG stats
    public StatsGroup stats;                        //character stats
    public ValuePool HP_Pool;

    public bool isAlive; //temporary for combat test

    // Start is called before the first frame update
    void Start()
    {   
        // Init character data
        attributes = new AttributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init();

        HP_Pool = new ValuePool(stats.Get(Statistic.HP));

        isAlive = true;
    }

    public void UpdateDataAfterCombat(CombatUnit unit)
    {
        state = unit.state;
        HP_Pool.currentValue = unit.currentHP;

        isAlive = unit.CheckAlive();
    }

    private void Update()
    {
        if(isAlive == false)
        {
            Destroy(gameObject);
        }
    }
}
