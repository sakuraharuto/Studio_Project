using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{   
    public static CharacterManager Instance = new CharacterManager();

    public List<string> cardList;  //card decker
    public void Init()
    {   
        // 
        cardList = new List<string>();
        cardList.Add("Attack");
        cardList.Add("Shield");
    }
}
