using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceVial : BaseVial
{
    public float slowValue;

    PlayerMovement playerMovement;

    public override void Initialize(int damage, float area, Vector2 target, Vector3 rotationSpeed, float timeToImpact, float vialDuration)
    {
        base.Initialize(damage, area, target, rotationSpeed, timeToImpact, vialDuration);

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    protected override void OnImpact()
    {
        VialEffects();
        Debug.DrawLine(transform.position, target, Color.blue, 4);
        Destroy(gameObject, vialDuration);
    }

    protected override void VialEffects()
    {
        circleCollider.enabled = true;
        spriteRenderer.sprite = areaSprite;
        spriteRenderer.color = Color.cyan;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Ice Area");
            playerMovement.Slow(slowValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Exited Ice Area");
            playerMovement.EndSlow();
        }
    }
}

