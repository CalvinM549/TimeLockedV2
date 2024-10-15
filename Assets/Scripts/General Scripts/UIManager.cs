using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public TextMeshProUGUI 
    public static UIManager instance;

    public UnityEngine.UI.Image fadeImage;
    public float fadeDuration;
    private bool isFading;

    public UnityEngine.UI.Image bossHealthLeft;
    public UnityEngine.UI.Image bossHealthRight;

    public UnityEngine.UI.Image playerHealthBar;
    public UnityEngine.UI.Image playerHealthBarGhost;

    //public TextMeshProUGUI ammoText;
    private float maxAmmo;
    public UnityEngine.UI.Image ammoImage;

    public Sprite[] HealChargeSpites;
    public UnityEngine.UI.Image healChargeImage;
    public TextMeshProUGUI healChargeText;

    public TextMeshProUGUI timerText;
    public UnityEngine.UI.Image timerImage;

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

        maxAmmo = player.GetComponent<PlayerController>().rangedAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();

        if (isFading)
        {
            Color currentColor = fadeImage.color;
            float fadeAmount = Time.deltaTime / fadeDuration;
            currentColor.a = Mathf.Clamp01(currentColor.a + fadeAmount);
            fadeImage.color = currentColor;

            if(currentColor.a >= 1)
            {
               isFading = false;
            }
        }
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(gameManager.timer / 60f);
        int seconds = Mathf.FloorToInt(gameManager.timer - minutes * 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerImage.fillAmount = gameManager.timer / gameManager.maxTime;
    }

    public void UpdateAmmoCount(int ammo)
    {
        //ammoText.text = (ammo.ToString());
        ammoImage.fillAmount = (float)ammo / maxAmmo;
    }

    public void UpdateHealCharges(int charges)
    {
        healChargeImage.sprite = HealChargeSpites[charges];
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

    public void StartFade()
    {
        isFading = true;
    }


}
