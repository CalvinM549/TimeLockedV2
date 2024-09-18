using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningVial : BaseVial
{
    // Start is called before the first frame update


    protected override void OnImpact()
    {
        VialEffects();
        Debug.DrawLine(transform.position, target, Color.yellow, 4);
        Destroy(gameObject, vialDuration);
    }

    protected override void VialEffects()
    {
        spriteRenderer.sprite = areaSprite;
        spriteRenderer.color = Color.yellow;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(target, area);
        foreach (Collider2D collider in colliders)
        {
            Debug.DrawLine(transform.position, collider.transform.position, Color.yellow, 2f);
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
