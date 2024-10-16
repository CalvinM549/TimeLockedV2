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
        anim.Play("BeastSlamAttackStart");
    }

    public override void ExecuteAttack()
    {
        // After windup
        attackInProgress = true;
        Debug.Log("ExecuteAttack");

        anim.SetTrigger("attackReturn");
        attackInProgress = false;

    }

    public void SpawnShockwave()
    {
        GameObject Shockwave = Instantiate(shockwavePrefab, transform.position, transform.rotation);
        Shockwave.GetComponent<ShockwavePrefabScript>().Initialize(expansionSpeed, maxAreaSize, shockwaveDuration, attackDamage);
    }

}
