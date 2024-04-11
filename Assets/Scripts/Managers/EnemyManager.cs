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
    public int[] enemyList;

    // spawn position
    public GameObject posObj;
    [SerializeField] List<GameObject> enemyPos;

    public GameObject[] enemyInScene;
    public EnemyState[] enemyStates;
    public int index;

    private void Awake()
    {
        instance = this;

        // Process enemy database
        allEnemy = Resources.LoadAll<EnemyData>("Enemy");
        enemyInSceneDict = enemyMapping.mapList.ToDictionary();
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
        enemyInScene = new GameObject[enemyList.Length];
        
        LoadEnemyPos();
        CreateEnemyInScene();
    }

    private void CreateEnemyInScene()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            //load enemy states if saved
            if (enemyStates.Any())
            {
                // enemy is eliminated, go next
                if (enemyStates[i] == null)
                {
                    enemyInScene[i] = null;
                    continue;
                }
                else  // enemy is alive update new state
                {
                    // create new enemy object
                    GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);
                    // get enemy config
                    EnemyData newData = GetEnemyData(enemyList[i]);
                    // attach enemy functional script
                    Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

                    //update enemy states
                    newEnemy.UpdateStates(enemyStates[i]);
                    newEnemy.enemyIndex = i;

                    enemyInScene[i] = obj;
                }
            }
            else // new enemies
            {
                // create new enemy object
                GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);
                // get enemy config
                EnemyData newData = GetEnemyData(enemyList[i]);
                // attach enemy functional script
                Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

                newEnemy.Init(newData);
                newEnemy.enemyIndex = i;
                enemyInScene[i] = obj;
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
        enemyStates = new EnemyState[enemyList.Length];

        // array
        for(int i = 0; i<enemyInScene.Length; i++)
        {   
            if(enemyInScene[i] != null)
            {
                Enemy enemyComponent = enemyInScene[i].GetComponent<Enemy>();

                enemyStates[i] = new EnemyState()
                {
                    enemyIndex = i,
                    data = enemyComponent.data,
                    isAlive = enemyComponent.isAlive,
                    attributes = enemyComponent.attributes,
                    stats = enemyComponent.stats,
                    HP_Pool = enemyComponent.HP_Pool
                };
            }
            else
            {
                enemyStates[i] = null;
            }
        }
    }

}



