using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    // enemy prefab
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemyData[] allEnemy;
    
    // enemy object
    [SerializeField] EnemyMapping enemyMapping;

    public Dictionary<string, int[]> enemyInSceneDict = new Dictionary<string, int[]>();
    int[] enemyList;

    // spawn position
    public GameObject posObj;
    [SerializeField] List<GameObject> enemyPos;

    public List<GameObject> enemyInScene;
    public List<EnemyState> enemyStates;
    public int index;

    private void Awake()
    {
        instance = this;

        // Process enemy database
        allEnemy = Resources.LoadAll<EnemyData>("Enemy");
        enemyInSceneDict = enemyMapping.mapList.ToDictionary();

        enemyStates = new List<EnemyState>();
    }

    // load enemies based on sceneName
    // Only invoked when player enter a building
    private int[] LoadEnemyList(string sceneName)
    {   
        if (enemyInSceneDict.TryGetValue(sceneName, out int[] enemyList))
        {
            return enemyList;
        }

        return null;
    }

    public void LoadEnemyPos()
    {
        enemyPos.Clear();

        if (posObj != null && posObj.transform.childCount >= enemyList.Length)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                enemyPos.Add(posObj.transform.GetChild(i).gameObject);
            }
        }
    }

    // spawn all enemies in the scene
    public void SpawnEnemy(string sceneName)
    {
        enemyList = LoadEnemyList(sceneName);
        
        LoadEnemyPos();
        CreateEnemyInScene();
    }

    private void CreateEnemyInScene()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);

            EnemyData newData = GetEnemyData(enemyList[i]);

            Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

            //check if need to load enemy states
            if(enemyStates.Any())
            {
                obj.GetComponent<Enemy>().UpdateStates(enemyStates[i]);

                if (enemyStates[i].isAlive == false) Destroy(obj);
            }
            else
            {   
                newEnemy.Init(newData);
                newEnemy.enemyIndex = i;
                enemyInScene.Add(obj);
            }
        }
    }

    private EnemyData GetEnemyData(int id)
    {
        foreach (EnemyData enemy in allEnemy)
        {
            if (enemy.enemyID == id)
                return enemy;
        }

        return null;
    }

    public void SaveEnemyStates()
    {
        enemyStates.Clear();
        foreach(var enemyObj in enemyInScene)
        {
            Enemy enemyComponent = enemyObj.GetComponent<Enemy>();
            if(enemyComponent != null)
            {
                enemyStates.Add(new EnemyState()
                {
                    isAlive = enemyComponent.isAlive,
                    currentHP = enemyComponent.HP_Pool.currentValue
                });
            }
        }
    }

}



