using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArtifact : MonoBehaviour
{
    private GameManager gameManager;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth <= 0)
        {
            gameManager.timerActive = false;
        }
    }
}
