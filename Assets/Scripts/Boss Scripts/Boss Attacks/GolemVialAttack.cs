//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemVialAttack : BossAttack
{

    public GameObject vialPrefab;
    public enum Vial { LIGHTNING, ACID, ICE }
    public Vial currentVial;

    public int lightningDamage;

    private int randomVial;
    private Transform currentTarget;

    public float areaSize;
    public float vialSpeed;
    public Vector3 vialRotationSpeed;


    public override void StartAttack(Transform target)
    {
        base.StartAttack(target);
        Debug.Log("StartAttack");

        //Array vialOptions = Enum.GetValues(typeof(Vial));
        currentTarget = target;
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;

        Transform finalTarget = currentTarget;

        StartCoroutine(GetRandomVials(finalTarget));

        Debug.Log("ExecuteAttack");
    }

    public void ThrowVial(Vial currentVial, Transform finalTarget)
    {
        GameObject vial = Instantiate(vialPrefab, transform.position, transform.rotation);


        float distance = Vector3.Distance(finalTarget.position, transform.position);
        float delay = distance * vialSpeed;
        Debug.Log("Spawn " + currentVial);
        vial.GetComponent<VialPrefabScript>().Initialize(finalTarget.position, vialRotationSpeed, delay, areaSize, lightningDamage, currentVial);
    }

    private IEnumerator GetRandomVials(Transform finalTarget)
    {
        int vialCount = Random.Range(0, 5);
        for (int i = 0; i < vialCount; i++)
        {
            currentVial = (Vial)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Vial)).Length);
            //currentVial = (Vial)values.getValue(Random.Range(0, vialOptions.Length));
            ThrowVial(currentVial, finalTarget);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
