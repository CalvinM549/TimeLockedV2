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

    public bool bossDefeated;
    public int currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        timerActive = true;
        timer = maxTime;

        bossDefeated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                sceneManager.GameEnd();
            }
        }


    }


}
