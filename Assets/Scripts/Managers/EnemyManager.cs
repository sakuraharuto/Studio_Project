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
    //public Dictionary<string, List<int>> enemyInSceneDict = new Dictionary<string, List<int>>();
    //[SerializeField] List<int> enemyList = new List<int>();

    public Dictionary<string, int[]> enemyInSceneDict = new Dictionary<string, int[]>();
    int[] enemyList;

    // spawn position
    public GameObject posObj;
    [SerializeField] List<GameObject> enemyPos;

    private void Awake()
    {
        instance = this;

        allEnemy = Resources.LoadAll<EnemyData>("Enemy");
        
        enemyInSceneDict = enemyMapping.mapList.ToDictionary();
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
    //private List<int> LoadEnemyList(string sceneName)
    private int[] LoadEnemyList(string sceneName)
    {   
        //if (enemyInSceneDict.TryGetValue(sceneName, out List<int> enemyList))
        //{ 
        //    return enemyList;
        //}

        if (enemyInSceneDict.TryGetValue(sceneName, out int[] enemyList))
        {
            return enemyList;
        }


        return null;
    }

    public void LoadEnemyPos()
    {
        enemyPos.Clear();

        //if(posObj != null && posObj.transform.childCount >= enemyList.Count)
        //{
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        enemyPos.Add(posObj.transform.GetChild(i).gameObject);
        //    }
        //}   
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

        //if(enemyPos != null)
        //{
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);

        //        EnemyData newData = GetEnemyData(enemyList[i]);

        //        Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

        //        newEnemy.Init(newData);
        //    }
        //}
        if (enemyPos != null)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                GameObject obj = Instantiate(enemyPrefab, enemyPos[i].transform);

                EnemyData newData = GetEnemyData(enemyList[i]);

                Enemy newEnemy = obj.AddComponent(System.Type.GetType(newData.enemyName)) as Enemy;

                newEnemy.Init(newData);
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
    
    public void UpdateEnemyList(string toSceneName, int enemyID)
    {

    }    

}
