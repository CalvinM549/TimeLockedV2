using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVial : BaseVial
{
    public bool playerTakingDamage;

    private Health playerHealth;

    // Start is called before the first frame update

    public override void Initialize(int damage, float area, Vector2 target, Vector3 rotationSpeed, float timeToImpact, float vialDuration)
    {
        base.Initialize(damage, area, target, rotationSpeed, timeToImpact, vialDuration);

        playerHealth = GameObject.Find("Player").GetComponent<Health>();

        StartCoroutine(AcidDamage());
    }

    // Update is called once per frame
    protected override void OnImpact()
    {
        VialEffects();
        Debug.DrawLine(transform.position, target, Color.green, 4);
        Destroy(gameObject, vialDuration);
    }

    protected override void VialEffects()
    {
        circleCollider.enabled = true;
        spriteRenderer.sprite = areaSprite;
        spriteRenderer.color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Acid Area");
            playerTakingDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Exited Acid Area");
            playerTakingDamage = false;
        }
    }

    private IEnumerator AcidDamage()
    {
        while(true)
        {
            if (playerTakingDamage)
            {
                playerHealth.TakeDamage(damage);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //spriteRenderer.sprite =
    //Sprite newSprite;
    //CircleCollider2D circleCollider2D = new CircleCollider2D();
    //circleCollider2D.enabled = true;
    //circleCollider2D.radius = areaSize;

}
