using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTrigger : MonoBehaviour
{
    public GameObject EnterCombatButton;
    [SerializeField] string postMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        EnterCombatButton.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {   
        EnterCombatButton.SetActive(true);
    }

    public void EnterCombat()
    {
        if(postMessage != null )
        {
            EnterCombatButton.SetActive(false);
            GameManager.instance.enemy = gameObject.GetComponent<Character>();
            GameSceneManager.instance.StartTransition(postMessage);
        }
    }    
}
