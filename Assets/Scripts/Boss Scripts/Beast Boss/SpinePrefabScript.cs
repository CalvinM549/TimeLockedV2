using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinePrefabScript : MonoBehaviour
{
    private Rigidbody2D rb;

    int damage;
    float speed;
    float inaccuracy;

    public void Initialize(int damage, float speed, float inaccuracy)
    {
        Debug.Log("SPINE SPAWNED");
        rb = GetComponent<Rigidbody2D>();

        this.damage = damage;
        this.speed = speed;
        this.inaccuracy = inaccuracy;

        StartMovement();
        Destroy(gameObject, 5f);
    }

    private void StartMovement()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
        if (!(collision.CompareTag("Boss")) || !(collision.CompareTag("Projectile")))
        {
            //Destroy(gameObject);
        }
    }
}
