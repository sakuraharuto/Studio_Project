using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { START, PLAYERTURN, ENEMYTURN, WIN, LOST, FLEE }

public class CombatManager : MonoBehaviour
{
    public TurnState state;

    Unit playerUnit;
    Unit enemyUnit;

    CardObject card;

    // Start is called before the first frame update
    void Start()
    {
        state = TurnState.START;
        SetupCombat();
    }
    
    void SetupCombat()
    {

    }

    void PlayerTurn()
    {   
        bool isDead = enemyUnit.CheckAlive();
        if (isDead)
        {
            state = TurnState.WIN;
            EndCombat();
        }
        else
        {
            state = TurnState.ENEMYTURN;
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        bool isDead = playerUnit.CheckAlive();
        if(isDead)
        {
            state = TurnState.LOST;
            EndCombat();
        }
        else
        {
            state = TurnState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndCombat()
    {   
        if(state == TurnState.WIN)
        {
            Debug.Log("You win!");
        }
        else if(state == TurnState.LOST)
        { 
            Debug.Log("You lost!");
        }
    }

    void Flee()
    {
        
    }
}
