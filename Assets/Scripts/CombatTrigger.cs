using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTrigger : MonoBehaviour
{
    public GameObject EnterCombatButton;
    
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
        SceneManager.LoadScene("Test_Combat");
        //GameSceneManager.instance.StartTransition("Test_Combat");
    }    
}
