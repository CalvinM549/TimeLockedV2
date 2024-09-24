using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossMeleeAttack : BossAttack
{
    private BossMovement movement;
    public float dashDistance;
    private Transform meleeTarget;
    private Vector2 targetPosition;


    public override void StartAttack(Transform target)
    {
        meleeTarget = target;
        targetPosition = target.position;
        Debug.Log("StartAttack");

        base.StartAttack(target);

        movement = GetComponent<BossMovement>();

        Debug.DrawLine(transform.position, targetPosition, Color.white, 5f);

    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");
        movement.Dash(targetPosition, dashDistance);
        attackInProgress = false;
    }

    // selects location to attack and enters windup
    // after windup, completes a short dash to target location and deals damage.
}
