using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    
    int damage;
    float speed;
    Vector2 direction;

    public void Initialize(int damage, float speed, Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();

        this.damage = damage;
        this.speed = speed; 
        this.direction = direction;

        StartMovement();
        Destroy(gameObject, 5f);
    }

    private void StartMovement()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        
        //if ((collision.CompareTag("Boss")) || collision.CompareTag("Projectile"))
        if(!(collision.CompareTag("Player")) && !(collision.CompareTag("GroundObject")) && !(collision.CompareTag("Vial")))
        {
            Destroy(gameObject);
        }
    }
}
