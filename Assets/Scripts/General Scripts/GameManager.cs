using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneManagerObj sceneManager;

    public static GameManager instance; 

    public float timer;
    public float maxTime;
    public bool timerActive;

    private int currentLevel;
    public bool bossOneDefeated;
    public bool bossTwoDefeated;
    public bool bossThreeDefeated;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        currentLevel = 0;

        timerActive = true;
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                sceneManager.GameReset();
            }
        }
    }

    public void CheckBoss()
    {

    }
}
