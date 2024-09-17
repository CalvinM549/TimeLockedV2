using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastSpineAttack : BossAttack
{
    public GameObject spineProjectile;

    public float angleVariation;
    public int spineCount;
    private Vector2 targetPosition;
    private Transform attackTarget;

    private float angleToTarget;
    private Quaternion angleToTargetQ;

    public override void StartAttack(Transform target)
    {
        attackTarget = target;
        targetPosition = target.position;
        Debug.Log("StartAttack");

        base.StartAttack(target);

        angleToTarget = Vector2.Angle(transform.position, targetPosition);
        angleToTargetQ = Quaternion.Euler(0, 0, angleToTarget);

        Debug.DrawLine(transform.position, targetPosition, Color.white, 5f);

    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");
        

        for(int currentSpine = 0; currentSpine < spineCount; currentSpine++)
        {
            GameObject spine = Instantiate(spineProjectile, transform.position, angleToTargetQ);
        }
    }

}
