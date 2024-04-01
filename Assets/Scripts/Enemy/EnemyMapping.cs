using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyList", menuName = "Combat System/Config/EnemyList")]

public class EnemyMapping : ScriptableObject
{
    [Header("Map Lists")]
    public NewDictionary mapList = new NewDictionary();
}
