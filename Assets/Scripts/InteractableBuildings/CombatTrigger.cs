using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatTrigger : MonoBehaviour
{
    public GameObject EnterCombatButton;
    [SerializeField] string postMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        EnterCombatButton = transform.Find("Canvas/Button").gameObject;
        postMessage = "test_Combat";
        EnterCombatButton.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {   
        EnterCombatButton.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        EnterCombatButton.SetActive(false);
    }

    public void EnterCombat()
    {
        if(postMessage != null )
        {
            EnterCombatButton.SetActive(false);
            EnemyManager.instance.SaveEnemyStates();
            GameSceneManager.instance.StartTransition(postMessage);
        }
    }    
}
