using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerObj : MonoBehaviour
{

    public static SceneManagerObj instance;


    public UIManager uiManager;
    private GameManager gameManager;
    public string currentLevelScene;
    public string[] levelNames;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Additive);
        currentLevelScene = "IntroScene";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextLevel()
    {
        LoadLevelScene(levelNames[gameManager.currentLevel]);
    }

    public void LoadLevelScene(string newLevel)
    {
        Debug.Log("Loading "+newLevel);
        SceneManager.UnloadSceneAsync("StairwellScene");
        SceneManager.LoadScene(newLevel, LoadSceneMode.Additive);
        currentLevelScene = newLevel;
        StartCoroutine(uiManager.GetNewObjects());
        gameManager.currentLevel++;
        gameManager.bossDefeated = false;
    }

    public void LoadTransition()
    {
        
        foreach(GameObject currentVial in GameObject.FindGameObjectsWithTag("Vial")){
            Destroy(currentVial);
        }
        Destroy(GameObject.FindWithTag("Corpse"));
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene("StairwellScene", LoadSceneMode.Additive);
    }

    public string CurrentSceneName()
    {
        return currentLevelScene;
    }
    
    public void GameEnd()
    {
        StartCoroutine(LoadToMenu("RespawnScene"));
    }
    public void GameWin()
    {
        StartCoroutine(LoadToMenu("VictoryScene"));
    }

    private IEnumerator LoadToMenu(string menuScene)
    {
        uiManager.StartFade();
        yield return new WaitForSeconds(uiManager.fadeDuration);
        SceneManager.LoadScene(menuScene);
    }


    // occurs when the player dies, or when the timer reaches 0

}

