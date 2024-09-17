using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastSpineAttack : BossAttack
{
    public GameObject spineProjectile;

    public float angleVariation;
    public int spineCount;
    private Vector2 vectorToTarget;
    private Transform target;

    private float rotationToTarget;
    private Quaternion angleToTargetQ;
    private float currentRotation;

    public float projectileSpeed;

    public override void StartAttack(Transform target)
    {
        this.target = target;
        Debug.Log("StartAttack");

        base.StartAttack(target);

        //angleToTarget = Vector2.Angle(transform.position, target.position);

        Debug.DrawLine(transform.position, target.position, Color.white, 5f);

        vectorToTarget = (transform.position - target.position);

        rotationToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        currentRotation = rotationToTarget;
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");
        

        for(int i = 0; i < spineCount; i++)
        {
            float angleVariationVal = Random.Range(-angleVariation, angleVariation);
            currentRotation = rotationToTarget + angleVariationVal;

            GameObject spine = Instantiate(spineProjectile, transform.position, Quaternion.Euler(0, 0, currentRotation + 90));
            spine.GetComponent<SpinePrefabScript>().Initialize(attackDamage, projectileSpeed, angleVariationVal);
        }
    }
}
