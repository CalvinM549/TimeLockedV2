using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerObj : MonoBehaviour
{
    public UIManager uiManager;

    public string currentScene;
    public string currentLevelScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Additive);
        currentLevelScene = "IntroScene";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            LoadTransition();
            LoadLevelScene("BossOneScene");    
        }
        if (Input.GetKeyDown("2"))
        {
            LoadTransition();
            LoadLevelScene("BossTwoScene");
        }
        if (Input.GetKeyDown("3"))
        {  
            LoadTransition();
            LoadLevelScene("BossThreeScene");
        }
    }

    public void LoadLevelScene(string newLevel)
    {
        SceneManager.UnloadSceneAsync("StairwellScene");
        SceneManager.LoadScene(newLevel, LoadSceneMode.Additive);
        currentLevelScene = newLevel;
        uiManager.GetNewObjects();
    }

    public void LoadTransition()
    {
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene("StairwellScene", LoadSceneMode.Additive);
    }

    

    // occurs when the player dies, or when the timer reaches 0
    public void GameReset()
    {
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene("IntroScene");
        SceneManager.LoadScene("CoreScene");
    }
}
