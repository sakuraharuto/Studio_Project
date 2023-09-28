using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
    [Header("Character Initial")]
    public GameObject player;
    public GameObject enemy;
    public Transform playerPosition;
    public Transform enemyPosition;

    [HideInInspector] public Unit playerUnit;
    [HideInInspector] public Unit enemyUnit;

    [Header("Turn State")]
    public TurnState state;

    [Header("Combat Config")]
    [SerializeField] CombatUI combatUI;
    [SerializeField] List<string> Deck;

    // Start is called before the first frame update
    void Start()
    {
        Deck = new List<string>();

        state = TurnState.START;
        //combatUI.Start(); 
        PlayerCardManager.Instance.Init();
        CardManager.Instance.Init();

        Deck.AddRange(CardManager.Instance.cardDeck);

        StartCoroutine(SetupCombat());
    }
    
    IEnumerator SetupCombat()
    {
        //Combat Start
        //Initial Positions of Player and Enemy 
        Instantiate(player, playerPosition);
        Instantiate(enemy, enemyPosition);
        playerUnit = player.GetComponent<Unit>();
        //combatUI.Start();

        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    //
    void TurnInitial()
    {
        Debug.Log("Start Combat !!!");
        //Draw cards
        //uiManager.GetUI<CombatUI>("CombatUI").CreateCardItem(3);
        //uiManager.GetUI<CombatUI>("CombatUI").UpdateCardPosition();
    }

    void PlayerTurn()
    {
        Debug.Log("Current Turn: " + state);
        //Check isAlive?
        //bool isDead = enemyUnit.CheckAlive();

        //Draw cards
        
        //uiManager.GetUI<CombatUI>("CombatUI").CreateCardItem(3);
        //uiManager.GetUI<CombatUI>("CombatUI").UpdateCardPosition();

        //use cards

        //drop cards

        //if (true)
        //{
        //    state = TurnState.WIN;
        //    //EndCombat();
        //}
        //else
        //{
        //    state = TurnState.ENEMYTURN;
        //    EnemyTurn();
        //}
    }

    void EnemyTurn()
    {
        Debug.Log("Current Turn: " + state);
        //bool isDead = playerUnit.CheckAlive();
        //if(isDead)
        //if (true)
        //{
        //    state = TurnState.LOST;
        //    EndCombat();
        //}
        //else
        //{
        //    state = TurnState.PLAYERTURN;
        //    PlayerTurn();
        //}
    }

    void EndCombat()
    {
        Debug.Log("Combat Summary");
        //if (state == TurnState.WIN)
        //{
        //    Debug.Log("You win!");
        //}
        //else if (state == TurnState.LOST)
        //{
        //    Debug.Log("You lost!");
        //}
    }

    void Flee()
    {
        Debug.Log("Flee Success.");
    }

    void Win()
    {
        Debug.Log("You Win !!!");
    }

    void Defeat()
    {
        Debug.Log("Defeat !!!");
    }

    public void TurnEnd()
    {   
        state = TurnState.ENEMYTURN;
        EnemyTurn();
    }

    public void DrawCardFromDeck()
    {
        Debug.Log("The card is: " + CardManager.Instance.DrawCard());

    }

    public void PrintDeck()
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            Debug.Log(Deck[i]);
        }
    }
}
