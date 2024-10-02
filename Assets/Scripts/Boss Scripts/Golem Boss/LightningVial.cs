using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningVial : BaseVial
{
    // Start is called before the first frame update


    protected override void VialEffects()
    {
        col = Color.yellow;
        col.a = 0.3f;
        spriteRenderer.color = col;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
