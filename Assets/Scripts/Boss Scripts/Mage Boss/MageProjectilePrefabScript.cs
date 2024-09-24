using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectilePrefabScript : MonoBehaviour
{
    
    private Rigidbody2D rb;

    int damage;
    float speed;
    float inaccuracy;
    public void Initialize(int damage, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        this.damage = damage;
        this.speed = speed;

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
        
        if (collision.CompareTag("Player") 
            || collision.CompareTag("Wall") 
            || collision.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
        }
    }
}

