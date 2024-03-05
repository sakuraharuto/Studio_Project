using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public enum SpecialStates
    {   
        Normal,
        LieShang,
        Stun,
        Pain
    }

    public string unitName;

    public SpecialStates state;
    public int maxHP;
    public int currentHP;
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
    //public void InitialData(Character unit)
    //{
    //    state = unit.state;
    //    maxHP = unit.currentHP;
    //    currentHP = maxHP;
    //    currentShield = 0;
    //    cost = 10;
    //}

    private void Update()
    {

    }

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
}
