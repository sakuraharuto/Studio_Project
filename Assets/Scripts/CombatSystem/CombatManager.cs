using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { START, PLAYERTURN, ENEMYTURN, WIN, LOST, FLEE }

public class CombatManager : MonoBehaviour
{
    //public static CombatManager instance = new CombatManager();

    public GameObject player;
    public GameObject enemy;

    public Transform playerPosition;
    public Transform enemyPosition;

    Unit playerUnit;
    Unit enemyUnit;

    public TurnState state;
    
    //CardObject card;

    // Start is called before the first frame update
    void Start()
    {
        state = TurnState.START;
        SetupCombat();
    }
    
    void SetupCombat()
    {
        //Combat Start
        //Initial Positions of Player and Enemy 
        Instantiate(player, playerPosition);
        Instantiate(enemy, enemyPosition);

        //Initial Player cards deck
    }

    void PlayerTurn()
    {   
        //Check isAlive?
        bool isDead = enemyUnit.CheckAlive();

        //Draw cards

        //use cards

        //drop cards

        
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
