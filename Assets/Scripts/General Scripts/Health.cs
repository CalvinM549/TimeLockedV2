using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public GameObject playerBloodEffect;
    
    private UIManager UIManager;
    private SceneManagerObj sceneManager;

    public CameraShake cameraShake;
    public Material bossMat;

    public float blendAmount;
    public float fadeSpeed;
    public AnimationCurve hitAnimation;

    private void Start()
    {
        //Sets Initial Health
        currentHealth = maxHealth;
        UIManager = UIManager.instance;
        sceneManager = SceneManagerObj.instance;
        if (bossMat != null)
        {
            StartCoroutine(Flash());
        }
    }
    IEnumerator Flash()
    {
        blendAmount = 0;
        bossMat.SetFloat("_BlendAmount", 0);
        while (true)
        {
            while (blendAmount > 0)
            {
                blendAmount -= Time.deltaTime * fadeSpeed;
                blendAmount = Mathf.Clamp(blendAmount, 0, 1);

                bossMat.SetFloat("_BlendAmount", hitAnimation.Evaluate(blendAmount));

                yield return null;
            }
            bossMat.SetFloat("_BlendAmount", 0);
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject);
        if(gameObject.tag == "Player")
        {
            cameraShake.ShakeCamera(damage/10);
            if(damage >= 10)
            {
                Debug.Log("BloodSplatter");
                GameObject bloodEffect = Instantiate(playerBloodEffect, transform.position, transform.rotation);
                Destroy(bloodEffect, 0.5f);
            }
        }

        if(bossMat != null)
        {
            blendAmount = 1;
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
            sceneManager.GameEnd();
            Destroy(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }
    }
}
