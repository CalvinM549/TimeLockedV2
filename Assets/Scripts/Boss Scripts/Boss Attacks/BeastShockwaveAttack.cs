using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastShockwaveAttack : BossAttack
{
    public float expansionSpeed;
    public float maxAreaSize;
    public float shockwaveDuration;

    public GameObject shockwavePrefab;
    private Vector3 startAreaSize;

    public override void StartAttack(Transform target)
    {
        // On windup effects
        Debug.Log("StartAttack");

        base.StartAttack(target);
    }

    public override void ExecuteAttack()
    {
        // After windup
        attackInProgress = true;
        Debug.Log("ExecuteAttack");

        GameObject Shockwave = Instantiate(shockwavePrefab, transform.position, transform.rotation);
        Shockwave.GetComponent<ShockwavePrefabScript>().Initialize(expansionSpeed, maxAreaSize, shockwaveDuration, attackDamage);
        attackInProgress = false;
    }



}
