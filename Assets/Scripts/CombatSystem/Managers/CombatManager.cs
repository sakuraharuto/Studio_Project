using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static Unit;

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
    DEFEAT, 
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
    public bool addedNewStateCard = false;

    #region
    /// <summary>
    /// Varaibles for test
    /// </summary>
    [SerializeField] private float timer;
    private float currentTime;
    public TMP_Text timerTXT;
    public TMP_Text turnTXT;
    public TMP_Text PlayerHP;
    public TMP_Text MonsterHP;

    public SpecialStates playerState;

    #endregion

    public void Init()
    {

    }

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;

        PlayerCardManager.instance.Init();
        CardManager.instance.Init();

        timerTXT.text = timer.ToString();

        StartCoroutine(SetupCombat());
    }

    IEnumerator SetupCombat()
    {
        state = TurnState.INITIAL;

        // instantiate characters
        Instantiate(player, playerPosition);
        Instantiate(enemy, enemyPosition);

        // initial characters data
        playerUnit = player.GetComponent<Unit>();
        playerUnit.InitialData();
        PlayerHP.text = playerUnit.currentHP.ToString();
        playerState = playerUnit.state;

        enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.InitialData();
        MonsterHP.text = enemyUnit.currentHP.ToString();

        // Initial Items
        //ItemMenu_Combat.instance.Init();

        yield return new WaitForSeconds(2f);
        
        CombatInitial();
    }

    // Initial player hand cards
    void CombatInitial()
    {
        // UIManager.Instance.GetUI<CombatUI>("CombatUI").CreateCardItem(3);   // initxial hand card
        // Instantiate(cardPrefab, handLeftPoint);
        //currentTime = timer;

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

        state = TurnState.PLAYERTURN;
    }

    // player turn
    IEnumerator PlayerTurn()
    {
        // reset player data 
        playerUnit.cost = 10;
        playerUnit.currentShield = 0;
        CardManager.instance.Shuffle();
        
        yield return new WaitForSeconds(1f);

        state = TurnState.PLAYERTURN;
        
        CombatUI.instance.UpdateCost();
        CombatUI.instance.UpdateShield();

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

    }

    private void Update()
    {   
        turnTXT.text = state.ToString();

        Deck = CardManager.instance.cardDeck;
        useDeck = CardManager.instance.usedDeck;

        playerState = playerUnit.state;

        // count down timer for player turn
        if (state == TurnState.ENEMYTURN)
        {
            if (currentTime <= 0)
            {
                currentTime = timer;
                timerTXT.text = currentTime.ToString("0");

                CheckUnitState(playerUnit);

                TurnEnd();
            }
            else
            {
                currentTime -= Time.deltaTime;
                timerTXT.text = currentTime.ToString("0");
            }
        }
    }

    // settle phase for each turn
    void TurnEnd()
    {   
        // settle enemy actions before switch to player turn
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
            // settle player actions before switch to enemy turn
            // check player special states
            // add special state cards into deck
            // settle player actions
            if (CheckAlive(enemyUnit))
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
                CombatUI.instance.DropHandCards();
            }

            TurnEnd();
        }
        else
        {
            Debug.Log("Cannot skip the turn");
        }
    }

    bool CheckAlive(Unit unit)
    {
        if(unit.currentHP <= 0)
        {
            return false;
        }
        else
        {
            return true;    
        }
    }

    // enemy actions
    void EnemyTurn()
    {
        state = TurnState.ENEMYTURN;
    }

    public void CheckUnitState(Unit player)
    {
        switch (playerState)
        {
            case SpecialStates.Normal:
                Debug.Log("Player State: " + playerUnit.state);
                addedNewStateCard = false;
                break;
            case SpecialStates.LieShang:
                // add LieShang card into player deck
                Debug.Log("Player State: " + playerUnit.state);
                if(!addedNewStateCard)
                {
                    CardManager.instance.cardDeck.Add("LieShang");
                    addedNewStateCard = true;
                }
                break;
            case SpecialStates.Stun:
                // do something
                Debug.Log("Player State: " + playerUnit.state);
                break;
            case SpecialStates.Pain:
                // do something
                Debug.Log("Player State: " + playerUnit.state);
                //CardManager.instance.cardDeck.Add("Pain");
                break;
        }
    }

    public void CombatExit()
    {
        // save combat results: Player data, monster alive?, loots

        // load map
    }

    #region Combat Summary
    // combat summary
    void EndCombat()
    {
        Debug.Log("Combat Summary");
    }

    public void Flee()
    {
        state = TurnState.FLEE;

        PlayerHP.text = "Escape success!";

        EndCombat();
    }

    void Win()
    {
        state = TurnState.WIN;

        PlayerHP.text = "You win!";

        EndCombat();
    }

    void Defeat()
    {
        state = TurnState.DEFEAT;

        PlayerHP.text = "Defeated";

        EndCombat();
    }
    #endregion

    /// <summary>
    /// Test Functions
    /// </summary>
    #region
    public void TestPlayerTakenDamage()
    {
        int dmg = 2;
        playerUnit.TakeDamage(dmg);
        UpdatePlayerInCombat();
        CombatUI.instance.UpdateShield();
    }

    public void UpdatePlayerInCombat()
    {
        PlayerHP.text = playerUnit.currentHP.ToString();
    }

    public void UpdateMonsterInCombat()
    {
        MonsterHP.text = enemyUnit.currentHP.ToString();
    }

    public void RemoveDebuff()
    {
        playerUnit.state = SpecialStates.Normal;
        Debug.Log(playerState);
    }

    public void SwitchToLS()
    {
        playerUnit.state = SpecialStates.LieShang;
        Debug.Log(playerState);
    }

    public void SwitchToPain()
    {
        playerUnit.state = SpecialStates.Pain;
        Debug.Log(playerState);
    }
    #endregion
}
