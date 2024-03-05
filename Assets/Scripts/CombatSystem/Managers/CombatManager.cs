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

    [Header("Character Config")]
    public GameObject player;
    public Transform playerPosition;
    public GameObject enemy;
    public Transform enemyPosition;

    [HideInInspector] public Unit playerUnit;
    [HideInInspector] public Unit enemyUnit;

    [Header("Turn State")]
    public TurnState state;
    [SerializeField] private int round;

    [Header("Combat Config")]
    [SerializeField] int count;

    [Header("Inspector Variables")]
    public SpecialStates playerState;
    public bool isPlayerTurn;
    public bool addedNewStateCard = false;
    [SerializeField] List<string> Deck = new List<string>();        //test
    [SerializeField] List<string> useDeck = new List<string>();     //test

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;

        timerTXT.text = timer.ToString();

        Init();
    }

    public void Init()
    {
        PlayerCardManager.instance.Init();
        CardManager.instance.Init();
        //ItemMenu_Combat.instance.Init();
        
        isPlayerTurn = true;
        round = 0;

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
        playerState = playerUnit.state;
        enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.InitialData();

        //test inspector panel
        PlayerHP.text = playerUnit.currentHP.ToString();
        MonsterHP.text = enemyUnit.currentHP.ToString();

        yield return new WaitForSeconds(2f);

        CombatInitial();
    }

    // Initial player hand cards
    void CombatInitial()
    {
        state = TurnState.PLAYERTURN;

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

        CheckUnitPreStates(playerUnit);
    }

    private void Update()
    {
        turnTXT.text = state.ToString();

        // for test
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

                //CheckUnitState(playerUnit);
                //TurnEnd();

                StartCoroutine(PostTurnProcess());
            }
            else
            {
                currentTime -= Time.deltaTime;
                timerTXT.text = currentTime.ToString("0");
            }
        }
    }

    private void PreTurnProcess()
    {
        Debug.Log("Turn State: Pre-Turn Process");
        
        round++;

        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            state = TurnState.PLAYERTURN;

            CheckUnitPreStates(playerUnit);

            CardManager.instance.Shuffle();
            CombatUI.instance.UpdateCost();
            CombatUI.instance.UpdateShield();
            CombatUI.instance.CreateCardItem(count);
            CombatUI.instance.UpdateCardPosition();
        }
        else
        {
            state = TurnState.ENEMYTURN;

            CheckUnitPreStates(enemyUnit);
            
            EnemyTurn();
        }
    }

    private void CheckUnitPreStates(Unit unit)
    {
        Debug.Log("Check Unit PreStates");
    }

    public void EndTurnButton()
    {
        if(state != TurnState.PLAYERTURN)
        {
            Debug.Log("Cannot skip the turn!");
        }
        else
        {
            Debug.Log("Player's turn ends");
            //drop all hand-cards
            if (CombatUI.instance.cardList.Count > 0)
            {
                CombatUI.instance.DropHandCards();
            }

            //reset timer
            currentTime = timer;
            timerTXT.text = timer.ToString();
            StartCoroutine(PostTurnProcess());
        }
    }

    IEnumerator PostTurnProcess()
    {
        Debug.Log("Turn State: Post-Turn Process");
        
        state = TurnState.END;

        if(isPlayerTurn)
        {
            CheckUnitPostStates(playerUnit);
            
            if (CheckUnitAlive(enemyUnit))
            {
                yield return new WaitForSeconds(3f);

                PreTurnProcess();
            }
            else
            {
                Win();
            }
        }
        else
        {
            CheckUnitPostStates(enemyUnit);
            if(CheckUnitAlive(playerUnit))
            {   
                CheckUnitState(playerUnit);

                playerUnit.cost = 10;
                playerUnit.currentShield = 0;

                yield return new WaitForSeconds(3f);

                PreTurnProcess();
            }
            else
            {
                Defeat();
            }
        }
    }

    private void CheckUnitPostStates(Unit unit)
    {
        Debug.Log("Check Unit PostStates");
    }
    
    bool CheckUnitAlive(Unit unit)
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

    public void CheckUnitState(Unit unit)
    {
        switch (unit.state)
        {
            case SpecialStates.Normal:
                Debug.Log("Player State: " + unit.state);
                addedNewStateCard = false;
                break;
            case SpecialStates.LieShang:
                // add LieShang card into player deck
                Debug.Log("Player State: " + unit.state);
                if(!addedNewStateCard)
                {
                    CardManager.instance.cardDeck.Add("LieShang");
                    addedNewStateCard = true;
                }
                break;
            case SpecialStates.Stun:
                // do something
                Debug.Log("Player State: " + unit.state);
                break;
            case SpecialStates.Pain:
                // do something
                Debug.Log("Player State: " + unit.state);
                //CardManager.instance.cardDeck.Add("Pain");
                break;
        }
    }

    // combat summary
    #region Combat Summary
    public void CombatExit()
    {
        // save combat results: Player data, monster alive?, loots

        // load map
    }

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
