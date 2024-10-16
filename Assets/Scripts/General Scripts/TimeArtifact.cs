using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArtifact : MonoBehaviour
{
    private GameManager gameManager;
    private Health health;
    private SceneManagerObj sceneManager;
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sceneManager = SceneManagerObj.instance;

    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth <= 0)
        {
            if(gameManager.timerActive && spriteRenderer.sprite != destroyedSprite)
            {
                gameManager.timerActive = false;
                spriteRenderer.sprite = destroyedSprite;
            }
            if (gameManager.bossDefeated)
            {
                sceneManager.GameWin();
            }
        }
    }
}
