using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConfigManager
{
    public static CombatConfigManager Instance = new CombatConfigManager();

    private CombatConfigData card;
    private CombatConfigData enemy;
    private CombatConfigData level;

    private TextAsset textAsset;

    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        card = new CombatConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemy = new CombatConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        level = new CombatConfigData(textAsset.text);
    }

    public List<Dictionary<string, string>> GetCardLines()
    {
        return card.GetLine();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemy.GetLine();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return level.GetLine();
    }

    public Dictionary<string, string> GetCardById(string id)
    {   
        return card.GetOneById(id);
    }

    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemy.GetOneById(id);
    }

    public Dictionary<string, string> GetLevelById(string id)
    {
        return level.GetOneById(id);
    }
}
