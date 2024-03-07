using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTrigger : MonoBehaviour
{
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {   
        canvas.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
    }

    public void EnterCombat()
    {
        GameSceneManager.instance.StartTransition("Test_Combat");
    }    
}
