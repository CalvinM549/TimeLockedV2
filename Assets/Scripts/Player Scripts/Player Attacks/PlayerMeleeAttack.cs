using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : PlayerAttack
{
    // Start is called before the first frame update
    public float meleeRange;
    public float meleeAngle;

    public new GameObject animation;
    public float sizeVal;

    public float angle;
    public Quaternion q;

    public override void StartAttack()
    {
        Debug.Log("MeleeAttackStart");
        base.StartAttack();

        // initialize windup things
    }

    public override void ExecuteAttack()
    {

        Vector3 mouseVector = GetComponent<PlayerController>().PlayerToMouse();
        
        //Debug.DrawRay(transform.position, mouseVector, Color.green, 2, false);

        angle = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg;
        q = Quaternion.Euler(0f, 0f, angle-90);

        GameObject anim = Instantiate(animation, transform.position, q);
        anim.transform.parent = transform;
        anim.transform.localScale = new Vector3(sizeVal, sizeVal, 0f);

        float damageDelay = anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2;

        StartCoroutine(delayDamage(damageDelay, mouseVector));

        
    }

    private IEnumerator delayDamage(float delay, Vector3 mouseVector)
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, meleeRange);
        foreach (Collider2D collider in hitColliders)
        {
            if (!(collider.CompareTag("Player")) && !(collider.CompareTag("Wall")))
            {
                //if ((Vector2.Angle(transform.position, collider.transform.position) - Vector2.Angle(transform.position, mouseVector) < mouseVector + meleeAngle))
                if (Vector2.Angle(mouseVector, (collider.transform.position - transform.position)) <= meleeAngle)
                {

                    Debug.DrawLine(transform.position, collider.transform.position, Color.red, 2, false);

                    if (collider.CompareTag("Boss") || collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<Health>().TakeDamage(attackDamage);

                        if (GetComponent<PlayerController>().rangedAmmo < 5)
                        {
                            GetComponent<PlayerController>().rangedAmmo++;
                        }
                    }

                }

                Debug.Log("Enemy Targeted");
            }

        }
    }

}
