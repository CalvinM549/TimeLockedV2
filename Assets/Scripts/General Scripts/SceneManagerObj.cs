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
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene("StairwellScene", LoadSceneMode.Additive);
    }

    public string CurrentSceneName()
    {
        return currentLevelScene;
    }
    
    public void GameEnd()
    {
        SceneManager.LoadScene("RespawnScene");
    }

    // occurs when the player dies, or when the timer reaches 0

}
