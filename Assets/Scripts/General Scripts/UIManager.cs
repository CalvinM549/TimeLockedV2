using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public TextMeshProUGUI 
    public static UIManager instance;


    public UnityEngine.UI.Image bossHealthLeft;
    public UnityEngine.UI.Image bossHealthRight;

    public UnityEngine.UI.Image playerHealthBar;
    public UnityEngine.UI.Image playerHealthBarGhost;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healChargeText;

    public TextMeshProUGUI timerText;

    private GameObject player;
    private GameObject boss;
    public GameManager gameManager;

    public int playerMaxHealth;
    public int bossMaxHealth;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        playerMaxHealth = player.GetComponent<Health>().maxHealth;

        ammoText.text = player.GetComponent<PlayerController>().rangedAmmo.ToString();
        healChargeText.text = player.GetComponent<PlayerController>().healCharges.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = gameManager.timer.ToString();
    }

    public void UpdateAmmoCount(int ammo)
    {
        ammoText.text = (ammo.ToString());
    }

    public void UpdateHealCharges(int charges)
    {
        healChargeText.text = (charges.ToString());
    }

    public IEnumerator GetNewObjects()
    {
        Debug.Log("NewBoss searching");
        yield return new WaitForSeconds(0.1f);
        GameObject newBoss = GameObject.FindWithTag("Boss");
        boss = newBoss;
        bossMaxHealth = newBoss.GetComponent<Health>().maxHealth;
        UpdateHealthBar(bossMaxHealth, boss);
        Debug.Log("NewBoss found");
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
