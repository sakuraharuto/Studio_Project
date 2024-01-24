using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
// Manager all character system
//  - Attribute, Statistics, Package



public class Character : MonoBehaviour
{
    [Header("Character Config")]
    public int speed;
    public int maxHP;
    public int currentHP;
    public int hpRegen;
    public int damge;
    public int Shield;

    [Header("Effects Config")]
    public bool isBleeding;
    public bool invulnerable;
    public bool pain;
    public bool burning;
    public bool isWeaken;
    
    //[SerializeField] AttributeGroup attributes;
    //[SerializeField] StatsGroup stats;

    // Start is called before the first frame update
    void Start()
    {   
        // Init character data
        //attribute = new AttributeGroup();
        //attribute.Init();

        //stats = new StatsGroup();
        //stats.Init();
    }
}
