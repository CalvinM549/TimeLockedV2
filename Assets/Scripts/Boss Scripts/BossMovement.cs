using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed;
    public bool movementEnabled;
    public Transform target;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(target != null && movementEnabled)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Vector2 moveVector = direction * movementSpeed * Time.fixedDeltaTime;
        
        //rb.MovePosition(rb.position + moveVector);
        rb.velocity += moveVector * Time.deltaTime * movementSpeed;
    }

    public void Dash(Vector2 dashTarget, float dashForce)
    {
        Vector2 dashDirection = ((Vector3)dashTarget - transform.position).normalized;
        Debug.DrawLine(transform.position, dashTarget, Color.magenta, 5f);

        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
    }

}
