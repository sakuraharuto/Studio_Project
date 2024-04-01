using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Characters/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Object Config")]
    public int enemyID;
    public string enemyName;
    public int enemyLevel;

    [Header("Combat Config")]
    public int maxHP;
    public int damage;
    public int shield;
}
