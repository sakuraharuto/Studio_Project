using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum TurnState 
{ 
    INITIAL, 
    END, 
    PLAYERTURN, 
    ENEMYTURN
}

public enum CombatOutcome
{ 
    WIN,
    DEFEAT,
    FLEE
}


public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [Header("Character Config")]
    public GameObject combatCharacter;
    public CombatUnit playerUnit;
    public CombatUnit enemyUnit;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform enemyPosition;
    private GameObject player;
    private GameObject enemy;

    [Header("Turn State")]
    public TurnState state;
    [SerializeField] private int round;

    [Header("Combat Config")]
    [SerializeField] int count;     // card count each turn
    public int lootDropCount;
    public List<int> lootList= new List<int>(); // to store loots

    [Header("UI Setting")]
    public GameObject LeavePanel;
    public TMP_Text combatOutcome;

    // will remove later
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
        LeavePanel.SetActive(false);

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
        player = Instantiate(combatCharacter, playerPosition.transform);
        enemy = Instantiate(combatCharacter, enemyPosition.transform);

        // initial characters data
        playerUnit = player.GetComponent<CombatUnit>();
        playerUnit.InitialUnitData(GameManager.instance.player.GetComponent<Character>());

        enemyUnit = enemy.GetComponent<CombatUnit>();
        enemyUnit.InitialUnitData(GameManager.instance.enemy.GetComponent<Character>());

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
        
        //delete later
        UpdateCombatInspector();
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

            playerUnit.RemoveShields();

            playerUnit.RestoreCost();
            CombatUI.instance.UpdateCost();

            CardManager.instance.Shuffle();
            
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
        if (state != TurnState.PLAYERTURN)
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

        if (isPlayerTurn)
        {
            CheckUnitPostStates(playerUnit);

            if (enemyUnit.CheckAlive())
            {
                yield return new WaitForSeconds(3f);

                PreTurnProcess();
            }
            else
            {
                yield return new WaitForSeconds(1f);
                
                EndCombat(CombatOutcome.WIN);
            }
        }
        else
        {
            CheckUnitPostStates(enemyUnit);

            if (playerUnit.CheckAlive())
            {
                CheckUnitState(playerUnit);

                yield return new WaitForSeconds(3f);

                PreTurnProcess();
            }
            else
            {
                EndCombat(CombatOutcome.DEFEAT);
            }
        }
    }

    private void CheckUnitPostStates(CombatUnit unit)
    {
        Debug.Log("Check Unit PostStates");
    }

    // enemy actions
    void EnemyTurn()
    {
        state = TurnState.ENEMYTURN;

        playerUnit.TakeDamage(10);

        PlayerHP.text = playerUnit.currentHP.ToString();
    }

    public void CheckUnitState(CombatUnit unit)
    {
        switch(unit.state)
        {
            case SpecialStates.Normal:
                Debug.Log("Player State: " + unit.state);
                addedNewStateCard = false;
                break;
            case SpecialStates.LieShang:
                // add LieShang card into player deck
                Debug.Log("Player State: " + unit.state);
                if (!addedNewStateCard)
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

    // Combat Summary
    public void EndCombat(CombatOutcome outcome)
    {
        state = TurnState.END;

        // update enemy object based on combat outcome
        switch (outcome)
        {   
            case CombatOutcome.WIN:

                combatOutcome.text = "You Win!";

                // do win outcome update
                EnemyManager.instance.enemyStates[GameManager.instance.enemyIndex] = null;

                break;

            case CombatOutcome.DEFEAT:

                combatOutcome.text = "Defeated!";

                // do defeat outcome update
                EnemyManager.instance.enemyStates[GameManager.instance.enemyIndex].currentHP = enemyUnit.currentHP;

                break;

            case CombatOutcome.FLEE:

                combatOutcome.text = "Escape Success!";

                // do flee outcome update
                EnemyManager.instance.enemyStates[GameManager.instance.enemyIndex].currentHP = enemyUnit.currentHP;

                break;
        }
        
        LeavePanel.SetActive(true);
    }

    public void CombatExit()
    {
        //Update Player stats after combat
        GameManager.instance.postCombatPlayerStats = playerUnit;
        
        GameSceneManager.instance.StartTransition(GameSceneManager.instance.previousScene);

        LeavePanel.SetActive(false);
    }

    /// <summary>
    /// Test Functions
    /// </summary>
    #region

    private void UpdateCombatInspector()
    {
        PlayerHP.text = playerUnit.currentHP.ToString();
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
