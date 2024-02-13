using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

// gameplay flow:
// 1.Start ==> Initial units position + Draw cards + show UI
// 2.InitialTurn ==> Draw cards from deck, 5 cards each turn
// 3.Player Turn
// 4.InitialTurn ==> Draws cards for enemy
// 5.Enemy Turn 
// 6.End = Win/Defeat/Flee
public enum TurnState 
{ 
    INITIAL, 
    END, 
    PLAYERTURN, 
    ENEMYTURN, 
    WIN, 
    LOST, 
    FLEE 
}

public class CombatManager : MonoBehaviour
{   
    public static CombatManager instance;

    [Header("Character Initial")]
    public GameObject player;
    public Transform playerPosition;
    public GameObject enemy;
    public Transform enemyPosition;

    [HideInInspector] public Unit playerUnit;
    [HideInInspector] public Unit enemyUnit;

    [Header("Turn State")]
    public TurnState state;

    [Header("Combat Config")]
    [SerializeField] int count;
    [SerializeField] List<string> Deck = new List<string>();        //test
    [SerializeField] List<string> useDeck = new List<string>();     //test
    [SerializeField] private float timer;
    private float currentTime;
    public TMP_Text timerTXT;
    public TMP_Text turnTXT;

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;

        count = 4;

        PlayerCardManager.instance.Init();
        CardManager.instance.Init();

        timerTXT.text = timer.ToString();

        StartCoroutine(SetupCombat());
    }

    IEnumerator SetupCombat()
    {
        state = TurnState.INITIAL;

        // instantiate characters
        //Instantiate(player, playerPosition);
        //Instantiate(enemy, enemyPosition);
        
        // initial characters data
        playerUnit = player.GetComponent<Unit>();
        playerUnit.cost = 10;
        playerUnit.currentShield = 0;
        enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.currentHP = 20;

        yield return new WaitForSeconds(2f);
        
        CombatInitial();
    }

    private void Update()
    {
        turnTXT.text = state.ToString();

        Deck = CardManager.instance.cardDeck;
        useDeck = CardManager.instance.usedDeck;

        // count down timer for player turn
        if (state == TurnState.ENEMYTURN)
        {
            if (currentTime <= 0)
            {
                currentTime = timer;
                timerTXT.text = currentTime.ToString("0");
                TurnEnd();
            }
            else
            {
                currentTime -= Time.deltaTime;
                timerTXT.text = currentTime.ToString("0");
            }
        }

        if (CardManager.instance.cardDeck.Count == 9)
        {
            
        }
    }

    // Initial player hand cards
    void CombatInitial()
    {
        // UIManager.Instance.GetUI<CombatUI>("CombatUI").CreateCardItem(3);   // initxial hand card
        // Instantiate(cardPrefab, handLeftPoint);
        //currentTime = timer;

        // Initial Items
        ItemMenu_Combat.instance.Init();

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

        state = TurnState.PLAYERTURN;
    }

    // enemy actions
    void EnemyTurn()
    {
        state = TurnState.ENEMYTURN;
    }

    // settle phase for each turn
    void TurnEnd()
    {
        // end of enemy turn
        if(state == TurnState.ENEMYTURN)
        {   
            //settle enemey actions
            if(CheckAlive(playerUnit))
            {
                state = TurnState.END;

                StartCoroutine(PlayerTurn());

            }
            else
            {
                Defeat();
            }
        }

        // end of player turn
        if(state == TurnState.PLAYERTURN)
        {
            // settle player actions
            if(CheckAlive(enemyUnit))
            {
                state = TurnState.END;
                
                EnemyTurn();
            }
            else
            {
                Win();
            }

        }
    }

    // player turn
    IEnumerator PlayerTurn()
    {
        // reset player data 
        playerUnit.cost = 10;
        playerUnit.currentShield = 0;
        CardManager.instance.Shuffle();

        yield return new WaitForSeconds(2f);

        state = TurnState.PLAYERTURN;
        
        CombatUI.instance.UpdateCost();
        CombatUI.instance.UpdateShield();

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

    }

    // empty hand-cards
    IEnumerator EmptyHand()
    {
        Debug.Log("Drop all cards in hand");
        CombatUI.instance.DropHandCards();

        yield return new WaitForSeconds(2f);
        EnemyTurn();
    }

    bool CheckAlive(Unit unit)
    {
        int hp = unit.currentHP;
        if(hp <= 0)
        {
            return false;
        }
        else
        {
            return true;    
        }
    }

    #region Combat Summary
    // combat summary
    void EndCombat()
    {
        Debug.Log("Combat Summary");
    }

    void Flee()
    {
        Debug.Log("Flee Success.");
        state = TurnState.FLEE;
    }

    void Win()
    {
        Debug.Log("You Win !!!");
        state = TurnState.WIN;
        EndCombat();
    }

    void Defeat()
    {
        Debug.Log("Defeat !!!");
        state = TurnState.LOST;
        EndCombat();
    }
    #endregion

    public void EndTurnButton()
    {   
        if(state == TurnState.PLAYERTURN)
        {
            //reset timer
            currentTime = timer;
            timerTXT.text = timer.ToString();

            //drop all hand-cards
            if (CombatUI.instance.cardList.Count > 0)
            {
                Debug.Log("Drop cards");
                CombatUI.instance.DropHandCards();
            }

            TurnEnd();
        }
        else
        {
            Debug.Log("Cannot skip the turn");
        }
    }

}
