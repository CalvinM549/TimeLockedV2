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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown("space"))
            {
                Dash();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity += new Vector2(horizontal, vertical) * Time.deltaTime * currentSpeed;
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

    private void Dash()
    {
        Vector2 dashTarget = GetComponent<PlayerController>().PlayerToMouse();
        Vector2 dashDirection = ((Vector3)dashTarget - transform.position).normalized;
        rb.AddForce(dashDirection * dashDistance, ForceMode2D.Impulse);
    }

    public void PushPlayer(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
