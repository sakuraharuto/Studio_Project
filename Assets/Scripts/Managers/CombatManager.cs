using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject enemy;
    [SerializeField]private Transform playerPosition;
    [SerializeField]private Transform enemyPosition;
    public CombatUnit playerUnit;
    public CombatUnit enemyUnit;

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

    private void Awake()
    {
        instance = this;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {   
        timerTXT.text = timer.ToString();
    }

    public void Init()
    {
        ItemMenu_Combat.instance.Init();
        PlayerCardManager.instance.Init();
        CardManager.instance.Init();
        
        isPlayerTurn = true;
        round = 0;

        StartCoroutine(SetupCombat());
    }

    IEnumerator SetupCombat()
    {
        state = TurnState.INITIAL;

        // instantiate characters
        Instantiate(player, playerPosition.transform);
        Instantiate(enemy, enemyPosition.transform);

        // initial characters data
        playerUnit = player.GetComponent<CombatUnit>();
        playerUnit.InitialUnitData(GameManager.instance.player.GetComponent<Character>());
        enemyUnit = enemy.GetComponent<CombatUnit>();
        enemyUnit.InitialUnitData(GameManager.instance.enemy.GetComponent<Character>());

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
            Debug.Log(playerUnit.maxHP);
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

    private void CheckUnitPreStates(CombatUnit unit)
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
            if (CombatUI.instance.inHandCards.Count > 0)
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
                yield return new WaitForSeconds(1f);
                Win();
            }
        }
        else
        {
            CheckUnitPostStates(enemyUnit);
            if(CheckUnitAlive(playerUnit))
            {   
                CheckUnitState(playerUnit);

                playerUnit.RestoreCost();
                playerUnit.RemoveShields();

                yield return new WaitForSeconds(3f);

                PreTurnProcess();
            }
            else
            {
                Defeat();
            }
        }
    }

    private void CheckUnitPostStates(CombatUnit unit)
    {
        Debug.Log("Check Unit PostStates");
    }
    
    bool CheckUnitAlive(CombatUnit unit)
    {
        if(unit.CheckAlive())
        {
            return true;
        }
        else
        {
            return false;    
        }
    }
     
    // enemy actions
    void EnemyTurn()
    {
        state = TurnState.ENEMYTURN;
    }

    public void CheckUnitState(CombatUnit unit)
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

        GameSceneManager.instance.StartTransition("Test_dc");

        //EndCombat();
    }

    void Win()
    {
        state = TurnState.WIN;

        PlayerHP.text = "You win!";

        SceneManager.LoadScene("Test_dc");

        //GameSceneManager.instance.StartTransition("Test_dc");

        //EndCombat();
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

    public void UpdateAllDataOnHUD()
    {
    
    }

    #endregion
}
