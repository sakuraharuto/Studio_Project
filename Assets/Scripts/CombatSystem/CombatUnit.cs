using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class CombatUnit : MonoBehaviour
{
    public string unitName;

    public SpecialStates state;
    public int currentHP;
    public int maxHP;
    public int currentShield;
    public int cost;

    public void InitialUnitData(Character character)
    {
        unitName = character.characterName;
        state = character.state;
        currentHP = character.HP_Pool.currentValue;
        maxHP = character.HP_Pool.maxValue.value;
        currentShield = 0;
        cost = 10;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Combat Interaction Functions
    /// </summary>
    #region
    public void TakeDamage(int dmg)
    {   
        if( currentShield >= dmg )
        {
            currentShield -= dmg;
        }
        else if( currentShield < dmg && currentShield >= 0 )
        {
            dmg -= currentShield;
            currentHP -= dmg;
        }
        else
        {   
            currentHP -= dmg;
        }
    }
    
    public void AddShield(int shield)
    {
        currentShield += shield;
    }    

    public bool CheckAlive()
    {
        if(currentHP <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetCost()
    {
        return cost;
    }

    public void UpdateCost(int value)
    {
        cost -= value;
    }

    public void RestoreCost()
    {
        cost = 10;
    }

    public void RemoveShields()
    {
        currentShield = 0;
    }

    #endregion
}
