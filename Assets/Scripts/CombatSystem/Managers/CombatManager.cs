using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum TurnState { INITIAL, END, PLAYERTURN, ENEMYTURN, WIN, LOST, FLEE }
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
    // public GameObject enemy;
    public Transform playerPosition;
    // public Transform enemyPosition;

    [HideInInspector] public Unit playerUnit;
    [HideInInspector] public Unit enemyUnit;

    [Header("Turn State")]
    public TurnState state;

    [Header("Combat Config")]
    [SerializeField] int count;
    [SerializeField] List<string> Deck;
    [SerializeField] private float timer;
    private float currentTime;
    public TMP_Text timerTXT;
    public TMP_Text turnTXT;

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;

        PlayerCardManager.instance.Init();
        CardManager.instance.Init();

        // test only
        Deck = new List<string>();
        Deck.AddRange(CardManager.instance.cardDeck);
        timerTXT.text = timer.ToString();

        StartCoroutine(SetupCombat());
    }

    IEnumerator SetupCombat()
    {
        state = TurnState.INITIAL;

        Instantiate(player, playerPosition);
        // Instantiate(enemy, enemyPosition);
        playerUnit = player.GetComponent<Unit>();
        //playerUnit.cost = 10;

        yield return new WaitForSeconds(2f);
        // yield return null;
        
        CombatInitial();

    }

    private void Update()
    {
        turnTXT.text = state.ToString();

        // count down timer for player turn
        if (state == TurnState.PLAYERTURN || state == TurnState.ENEMYTURN)
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

    }

    // Initial player hand cards
    void CombatInitial()
    {
        // UIManager.Instance.GetUI<CombatUI>("CombatUI").CreateCardItem(3);   // initxial hand card
        // Instantiate(cardPrefab, handLeftPoint);
        currentTime = timer;

        playerUnit.cost = 10;

        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

        state = TurnState.PLAYERTURN;
    }

    // player turn
    IEnumerator PlayerTurn()
    {
        state = TurnState.END;

        // reset player data 
        playerUnit.cost = 10;
        CardManager.instance.Shuffle();

        yield return new WaitForSeconds(2f);
        
        CombatUI.instance.UpdateCost();
        CombatUI.instance.CreateCardItem(count);
        CombatUI.instance.UpdateCardPosition();

        state = TurnState.PLAYERTURN;
    }

    // enemy actions
    void EnemyTurn()
    {
        state = TurnState.ENEMYTURN;

        // do some actions

    }

    // settle phase for each turn
    void TurnEnd()
    {   
        StopAllCoroutines();
        if(state == TurnState.ENEMYTURN)
        {
            //settle enemey actions

            StartCoroutine(PlayerTurn());
        }

        if(state == TurnState.PLAYERTURN)
        {
            // settle player actions

            // chech character alive ? continue : end

            // drop hand-cards
            state = TurnState.END;

            //StopAllCoroutines();
            StartCoroutine(EmptyHand());
        }
    }

    // empty hand-cards
    IEnumerator EmptyHand()
    {
        Debug.Log("Drop all cards in hand");
        CombatUI.instance.DropHandCards();

        yield return new WaitForSeconds(2f);
        EnemyTurn();
    }

    void EndCombat()
    {
        Debug.Log("Combat Summary");
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

    public void DrawCardFromDeck()
    {
        Debug.Log("click");
        
        if(CardManager.instance.HasCards())
        {
            CombatUI.instance.CreateCardItem(1);
            CombatUI.instance.UpdateCardPosition();
        }
        else
        {
            Debug.Log("No un-used cards");
        }
    }

    public void PrintDeck()
    {
        Debug.Log(CardManager.instance.usedDeck.Count);
    }

    public void CheckDeck()
    {
        Debug.Log(CardManager.instance.cardDeck.Count);
    }

    public void EndTurnButton()
    {   
        if(state == TurnState.PLAYERTURN)
        {   
            //reset timer
            currentTime = timer;
            timerTXT.text = timer.ToString();

            //drop all hand-cards
            CombatUI.instance.DropHandCards();
            CardManager.instance.Shuffle();
            TurnEnd();
        }
    }

    public void TakeDamage(int dmg)
    {
        //Debug.Log("Take " + dmg + " damage from the card");
    }

    public void ShuffleCards()
    {
        CardManager.instance.Shuffle();
    }
}
