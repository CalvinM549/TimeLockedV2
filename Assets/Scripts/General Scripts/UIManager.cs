using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public TextMeshProUGUI 

    public UnityEngine.UI.Image bossHealthLeft;
    public UnityEngine.UI.Image bossHealthRight;

    public UnityEngine.UI.Image playerHealthBar;
    public UnityEngine.UI.Image playerHealthBarGhost;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healChargeText;

    public TextMeshProUGUI timerText;

    private GameObject player;
    private GameObject boss;
    public GameObject GameManager;

    public int playerMaxHealth;
    public int bossMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");

        playerMaxHealth = player.GetComponent<Health>().maxHealth;
        bossMaxHealth = boss.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoCount(int ammo)
    {
        ammoText.text = (ammo.ToString());
    }

    public void UpdateHealCharges(int charges)
    {
        healChargeText.text = (charges.ToString());
    }

    public void UpdateTimerText()
    {
        
    }

    public void UpdateHealthBar(int newHealth, GameObject damagedObject)
    {

        if (damagedObject.CompareTag("Player"))
        {
            Debug.Log("Player Healthbar Updated");

            playerHealthBar.fillAmount = ((float)newHealth / (float)playerMaxHealth);

            int playerHeal = player.GetComponent<PlayerController>().healAmount;
            playerHealthBarGhost.fillAmount = ((float)(newHealth+playerHeal) / (float)playerMaxHealth);
        }

        if (damagedObject.CompareTag("Boss"))
        {
            Debug.Log("Boss Healthbar Updated");

            bossHealthLeft.fillAmount = ((float)newHealth / (float)bossMaxHealth);
            bossHealthRight.fillAmount = ((float)newHealth / (float)bossMaxHealth);
        }
    }
}
