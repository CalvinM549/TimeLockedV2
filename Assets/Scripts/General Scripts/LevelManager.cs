using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isTransitionScene;
    public Collider2D exitCollider;
    public Vector2 spawnPosition;

    private PlayerController player;
    private SceneManagerObj sceneManager;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        sceneManager = SceneManagerObj.instance;
        player = PlayerController.instance;
        gameManager = GameManager.instance;

        player.transform.position = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager.bossDefeated && other.CompareTag("Player"))
        {
            if (isTransitionScene)
            {
                sceneManager.LoadNextLevel();
            }
            else
            {
                sceneManager.LoadTransition();
            }
        }
    }
}
