using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{   
    public static GameSceneManager instance;

    [SerializeField] string currentScene;
    [SerializeField] GameObject LoadPanel;
    [SerializeField] Animator animLoad;

    AsyncOperation load;
    AsyncOperation unload;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != "Global") //NavigationMap
            {
                currentScene = SceneManager.GetSceneAt(i).name;
                break;
            }
        }

        animLoad = LoadPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition(string toSceneName) 
    {
        StartCoroutine(TransitionScene(toSceneName));
    }

    public IEnumerator TransitionScene(string toSceneName) 
    {
        animLoad.SetTrigger("In");
        yield return new WaitForSeconds(1f);    

        SwitchScene(toSceneName);

        while (load.isDone == false && unload.isDone == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        animLoad.SetTrigger("Out");
        yield return new WaitForSeconds(1f);

        load = null;
        unload = null;

    }

    public void SwitchScene(string toSceneName)
    {
        load = SceneManager.LoadSceneAsync(toSceneName, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = toSceneName;
    }


}
