using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MageLaserAttack : BossAttack
{
    private float elapsedTime;
    public float stopTrackingDelay;
    public float beamLingerTime;

    public float beamLength;
    public float beamWidth;

    private Vector2 targetDirection;
    private float angle;

    private Transform target;
    private Vector2 currentTarget;
    private Vector2 finalTarget;
    private LineRenderer lr;

    public override void StartAttack(Transform target)
    {
        base.StartAttack(target);
        this.target = target;
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        StartCoroutine(DoWarning());
    }
    public override void ExecuteAttack()
    {
        attackInProgress = true;
        lr.startWidth = beamWidth;
        lr.endWidth = beamWidth;


        targetDirection = (finalTarget - (Vector2)transform.position).normalized;
        angle = Vector2.SignedAngle(Vector2.right, targetDirection);
        
        Vector2 beamCenter = ((Vector2)transform.position + (targetDirection * beamLength));
        Vector2 size = new Vector2(beamLength * 2, beamWidth);

        lr.SetPosition(1, beamCenter*2);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(beamCenter, size, angle);
        if (colliders[0] != null)
        {
            foreach(Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.GetComponent<Health>().TakeDamage(attackDamage);
                }
            }
        }

        Invoke("DisableBeam", beamLingerTime);
        attackInProgress = false;
    }

    private IEnumerator DoWarning()
    {
        currentTarget = target.position;
        while (elapsedTime < windupTime - stopTrackingDelay)
        {
            elapsedTime += Time.deltaTime;
            lr.SetPosition(1, currentTarget);
            yield return null;
            currentTarget = target.position;

        }
        finalTarget = target.position;
        elapsedTime = 0;
    }

    private void DisableBeam()
    {
        lr.enabled = false;
    }
}
