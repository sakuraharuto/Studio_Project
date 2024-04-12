using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
        // assign visual element
        gameObject.GetComponent<Image>().sprite = character.combatSprite;

        // assign character stats 
        unitName = character.characterName;
        state = character.state;
        maxHP = character.HP_Pool.maxValue.value;
        currentHP = character.HP_Pool.currentValue;
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
            gameObject.GetComponent<CharacterHUD>().shieldIcon.sprite = gameObject.GetComponent<CharacterHUD>().shieldBroken;

            dmg -= currentShield;
            currentHP -= dmg;
            currentShield = 0;
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
