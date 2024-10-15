using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArtifact : MonoBehaviour
{
    private GameManager gameManager;
    private Health health;
    private SceneManagerObj sceneManager;
    public GameObject destroyedArtifact;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        health = GetComponent<Health>();
        sceneManager = SceneManagerObj.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth <= 0 && gameManager.bossDefeated)
        {
            gameManager.timerActive = false;
            Instantiate(destroyedArtifact, transform.position, transform.rotation);
            sceneManager.GameWin();
            Destroy(gameObject);
        }
    }
}
