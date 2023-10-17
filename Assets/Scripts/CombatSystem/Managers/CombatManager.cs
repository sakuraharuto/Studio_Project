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
    public static CombatManager instance;

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
    //[SerializeField] CombatUI combatUI;
    [SerializeField] List<string> Deck;

    //[Header("test")]
    


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        state = TurnState.START;
        Deck = new List<string>();

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

        //yield return new WaitForSeconds(2f);
        yield return null;
        //PlayerTurn();
        TurnInitial();
    }

    void TurnInitial()
    {
        // Debug.Log("Prepare for player's turn");
        // UIManager.Instance.GetUI<CombatUI>("CombatUI").CreateCardItem(3);   // initxial hand card
        // Instantiate(cardPrefab, handLeftPoint);

        CombatUI.instance.CreateCardItem(3);
        CombatUI.instance.UpdateCardPosition();

    }

    void PlayerTurn()
    {
        Debug.Log("Current Turn: " + state);
        //Check isAlive?
        //bool isDead = enemyUnit.CheckAlive();

        //Draw cards

        //UIManager.Instance.GetUI<CombatUI>("Card").CreateCardItem(3);
        //UIManager.Instance.GetUI<CombatUI>("Card").UpdateCardPosition();

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
        //UIManager.Instance.GetUI<CombatUI>("Card").CreateCardItem(1);
    }

    public void PrintDeck()
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            Debug.Log(Deck[i]);
        }
    }
}
