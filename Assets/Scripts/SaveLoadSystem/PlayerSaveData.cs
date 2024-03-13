using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData myData = new PlayerData();
    //public Character player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        myData.playerPosition = transform.position;
        myData.playerRotation = transform.rotation;

        myData.currentHP = GetComponent<Character>().HP_Pool.currentValue;
    }
}

[System.Serializable]
public struct PlayerData
{
    [Header("Character Position Data")]
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    [Header("Character Stats")]
    public SpecialStates currentState;
    public int currentHP;
}

