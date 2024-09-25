using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    private UIManager UIManager;

    public CameraShake cameraShake;

    private void Start()
    {
        //Sets Initial Health
        currentHealth = maxHealth;
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject);
        if(gameObject.tag == "Player")
        {
            cameraShake.ShakeCamera(damage/10);
        }

        currentHealth -= damage;
        UIManager.UpdateHealthBar(currentHealth, this.gameObject);
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void GainHealth(int amount)
    {
        if (currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = maxHealth;
        }

        UIManager.UpdateHealthBar(currentHealth, this.gameObject);
    }

    public void Death()
    {
        if(gameObject.name == "TimeArtifact")
        {
            //
        }

        else if (gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }
    }
}
