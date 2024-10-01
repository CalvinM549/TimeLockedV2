using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : PlayerAttack
{
    // Start is called before the first frame update
    public float meleeRange;
    public float meleeAngle;


    public override void StartAttack()
    {
        Debug.Log("MeleeAttackStart");
        base.StartAttack();

        // initialize windup things
    }

    public override void ExecuteAttack()
    {
        Vector3 mouseVector = GetComponent<PlayerController>().PlayerToMouse();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, meleeRange);
        Debug.DrawRay(transform.position, mouseVector, Color.green, 2, false);

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
