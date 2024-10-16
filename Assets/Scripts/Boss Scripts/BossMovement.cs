using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed;
    public bool movementEnabled;
    private Vector2 moveVector;

    public Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public bool defaultRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            anim.SetBool("isMoving", true);
            MoveTowardsTarget();
            UpdateSprite();
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        moveVector = direction * movementSpeed * Time.fixedDeltaTime;
        
        //rb.MovePosition(rb.position + moveVector);
        rb.velocity += moveVector * Time.deltaTime * movementSpeed;
    }

    public void Dash(Vector2 dashTarget, float dashForce)
    {
        Vector2 dashDirection = ((Vector3)dashTarget - transform.position).normalized;
        Debug.DrawLine(transform.position, dashTarget, Color.magenta, 5f);

        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
    }
    private void UpdateSprite()
    {
        if (moveVector.x < 0)
        {
            spriteRenderer.flipX = defaultRight == true ? true : false;
        }
        else
        {
            spriteRenderer.flipX = defaultRight == true ? false : true;
        }
    }
}
