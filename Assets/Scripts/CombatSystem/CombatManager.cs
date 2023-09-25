using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { START, INITIAL, PLAYERTURN, ENEMYTURN, WIN, LOST, FLEE }
// gameplay flow:
// 1.Start ==> Initial units position + Draw cards + show UI
// 2.InitialTurn ==> Draw cards from deck, 5 cards each turn
// 3.Player Turn
// 4.InitialTurn ==> Draws cards for enemy
// 5.Enemy Turn 
// 6.End = Win/Defeat/Flee

public class CombatManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerPosition;
    public Transform enemyPosition;

    Unit playerUnit;
    Unit enemyUnit;

    public TurnState state;

    [SerializeField]
    PlayerCardManager playerCardManager;
    //CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        state = TurnState.START;
        playerCardManager.Init();
        StartCoroutine(SetupCombat());
    }
    
    IEnumerator SetupCombat()
    {
        //Combat Start
        //Initial Positions of Player and Enemy 
        Instantiate(player, playerPosition);
        Instantiate(enemy, enemyPosition);

        //Initial Player cards deck

        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    //
    void TurnInitial()
    {
        //cardManager.InitCards();
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
