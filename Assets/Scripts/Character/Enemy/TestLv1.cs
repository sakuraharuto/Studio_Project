using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLv1 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {   
        characterName = data.enemyName;
        combatSprite = data.combatSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
