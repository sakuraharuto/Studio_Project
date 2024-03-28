using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] EnemyData[] allEnemies;

    MappingDictionary enemyMapping;
    
    [SerializeField] List<int> enemyList = new List<int>();
    [SerializeField] List<GameObject> enemyPos = new List<GameObject>();

    public GameObject posObj;

    private void Awake()
    {
        instance = this;

        allEnemies = Resources.LoadAll<EnemyData>("Enemy");

        enemyMapping = gameObject.GetComponent<MappingDictionary>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // load enemies based on sceneName
    // Only invoked when player enter a building
    private List<int> LoadEnemyList(string sceneName)
    {
        //Load enemy list for corresponding scene
        foreach(KeyValuePair<string, List<int>> pair in enemyMapping.thisDictionary)
        {
            if (pair.Key == sceneName)
            {
                return pair.Value;
            }
        }

        return null;
    }

    public void LoadEnemyPos()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Debug.Log(posObj);
            if(posObj != null)
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

        if(enemyPos != null)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);

                EnemyData data = GetEnemyData(enemyList[i]);

                Enemy newEnemy = obj.AddComponent(System.Type.GetType(data.enemyName)) as Enemy;

                newEnemy.Init(data);
            }
        }
    }
    
    private EnemyData GetEnemyData(int id)
    {   
        foreach(EnemyData enemy in allEnemies)
        {
            if(enemy.enemyID == id)
                return enemy;
        }

        return null;
    }

    //// eliminate enemy obj after player kills
    //public void RemoveEnemyByID(int id)
    //{
            
    //}
}
