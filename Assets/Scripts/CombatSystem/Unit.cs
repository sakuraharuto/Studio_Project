using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int maxHP;
    public int currentHP;
    public int currentShield;
    [SerializeField] public int cost = 10;
    //public int damage;
    //public int shield;

    //public List<Card> UsedDeck;
    //public List<Card> NewDeck;

    public void TakeDamage(int dmg)
    {   
        if( currentShield >= dmg )
        {
            currentShield -= dmg;
            Debug.Log("Current Shield: " + currentShield);
            Debug.Log("\nCurrent Hp: " + currentHP);
        }
        else if( currentShield < dmg && currentShield >= 0)
        {
            dmg -= currentShield;
            currentHP -= dmg;
            Debug.Log("Current Shield: " + currentShield);
            Debug.Log("\nCurrent Hp: " + currentHP);
        }
        else
        {   
            currentHP -= dmg;
            Debug.Log("Current Shield: " + currentShield);
            Debug.Log("\nCurrent Hp: " + currentHP);
        }
    }
    
    public void AddShield(int shield)
    {
        currentShield += shield;
        Debug.Log("Current Shield: " + currentShield);
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
