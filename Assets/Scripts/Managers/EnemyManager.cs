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

    public List<GameObject> enemyInScene;
    //public List<EnemyState> enemyStates = new List<EnemyState>();   
    public List<EnemyState> enemyStates;

    // spawn position
    public GameObject posObj;
    [SerializeField] List<GameObject> enemyPos;

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
 
        if(!enemyStates.Any())
        {
            CreateEnemyInScene();
        }
        else
        {
            CreateEnemyInScene();
            LoadEnemyStates();
        }

    }

    private void CreateEnemyInScene()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            Debug.Log("Spawn Enemy");

            GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);

            EnemyData newData = GetEnemyData(enemyList[i]);

            Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

            newEnemy.Init(newData);

            enemyInScene.Add(obj);
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

    public void LoadEnemyStates()
    {
        int i = 0;
        foreach(GameObject enemyObj in enemyInScene)
        {   
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.UpdateStates(enemyStates[i]);
            i++;
        }
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
                    pos = enemyObj.transform.position,
                    currentHP = enemyComponent.HP_Pool.currentValue
                });
            }
        }
    }
}

//[System.Serializable]
//public class EnemyState
//{
//    public int enemyID;
//    public Vector3 pos;
//    public bool isAlive;
//    public int currentHP;
//}



