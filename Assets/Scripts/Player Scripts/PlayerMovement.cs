using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed;
    public float baseSpeed;

    private float vertical;
    private float horizontal;
    private float dashDistance;

    public bool movementEnabled;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        UpdateSprite();
    }

    void FixedUpdate()
    {
        Vector2 movementDirection = new Vector2(horizontal, vertical);
        rb.velocity = movementDirection * currentSpeed;

        //original movement
        //rb.velocity += new Vector2(horizontal, vertical) * Time.deltaTime * currentSpeed;
    }

    public void Slow(float slowValue)
    {
        if(currentSpeed == baseSpeed)
        {
            currentSpeed *= slowValue;
        }
    }

    public void EndSlow()
    {
        currentSpeed = baseSpeed;
    }


    public void PushPlayer(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }


    private void UpdateSprite()
    {
        if (horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }

        if(horizontal != 0 || vertical != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
