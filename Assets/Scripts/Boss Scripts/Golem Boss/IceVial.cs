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

    }


    protected override void VialEffects()
    {
        col = Color.cyan;
        col.a = 0.3f;
        spriteRenderer.color = col;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Ice Area");
            collision.GetComponent<PlayerMovement>().Slow(slowValue);
            
            //playerMovement.Slow(slowValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Exited Ice Area");
            collision.GetComponent<PlayerMovement>().EndSlow();
        }
    }
}

