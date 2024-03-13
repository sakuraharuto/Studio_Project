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

    public void InitialData()
    {   
        state = SpecialStates.Normal;
        maxHP = 10;
        currentHP = maxHP;
        currentShield = 0;
        cost = 10;
    }

    //prepare for character system
    public void InitialData(CombatUnit unit)
    {
        unitName = unit.unitName;
        state = unit.state;
        //state = SpecialStates.Normal;
        maxHP = unit.maxHP;
        currentHP = unit.currentHP;
        currentShield = unit.currentShield;
        cost = 10;
        
        // for Character script
        //maxHP = unit.stats.Get(Statistic.HP).value;
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

        //test
        CombatManager.instance.UpdatePlayerInCombat();
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
