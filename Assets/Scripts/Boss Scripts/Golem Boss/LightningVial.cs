using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningVial : BaseVial
{
    // Start is called before the first frame update


    protected override void VialEffects()
    {
        spriteRenderer.sprite = areaSprite;
        spriteRenderer.color = Color.yellow;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(target, area/2);
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
