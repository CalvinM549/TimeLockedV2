    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWaveAttack : BossAttack
{
    public GameObject projectile;

    private float angleShift;
    public float waveAngle;
    public int projectileCount;

    private Vector2 vectorToTarget;
    private Transform target;

    private float rotationToTarget;
    private float currentRotation;

    public float projectileSpeed;

    public override void StartAttack(Transform target)
    {
        this.target = target;
        Debug.Log("StartAttack");

        if (gameObject.CompareTag("Boss"))
        {
            base.StartAttack(target);
        }


        vectorToTarget = (transform.position - target.position);

        rotationToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        currentRotation = rotationToTarget;
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");
        angleShift = waveAngle / projectileCount;
        currentRotation = rotationToTarget - waveAngle/2;

        for (int i = 0; i < projectileCount; i++)
        {
            currentRotation += angleShift;

            GameObject mageProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, currentRotation + 90));
            mageProjectile.GetComponent<MageProjectilePrefabScript>().Initialize(attackDamage, projectileSpeed);
        }
        attackInProgress = false;
    }
}
