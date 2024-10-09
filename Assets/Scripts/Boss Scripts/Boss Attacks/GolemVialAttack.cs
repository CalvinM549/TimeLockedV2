//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemVialAttack : BossAttack
{

    public GameObject vialPrefab;
    public GameObject lightningVialPrefab;
    public GameObject acidVialPrefab;
    public GameObject iceVialPrefab;

    private GameObject vial;

    public int minVials;
    public int maxVials;

    public enum Vial { LIGHTNING, ACID, ICE }
    public Vial currentVial;

    public int lightningDamage;
    public float lightningDuration;

    public float iceDuration;

    public int acidDamage;
    public float acidDuration;

    private Transform currentTarget;

    public float areaSize;
    public float vialSpeed;
    public Vector3 rotationSpeed;


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


        float distance = Vector3.Distance(finalTarget.position, transform.position);
        float delay = distance * vialSpeed;
        Debug.Log("Spawn " + currentVial);
        
        switch (currentVial)
        {
            //int damage, float area, Vector2 target, Vector3 rotation, float timeToImpact

            case Vial.LIGHTNING:
                vial = Instantiate(lightningVialPrefab, transform.position, transform.rotation);
                vial.GetComponent<LightningVial>().Initialize(lightningDamage, areaSize, finalTarget.position, rotationSpeed, delay, lightningDuration);
                break;
            case Vial.ACID:
                vial = Instantiate(acidVialPrefab, transform.position, transform.rotation);
                vial.GetComponent<AcidVial>().Initialize(acidDamage, areaSize, finalTarget.position, rotationSpeed, delay, acidDuration);
                break;
            case Vial.ICE:
                vial = Instantiate(iceVialPrefab, transform.position, transform.rotation);
                vial.GetComponent<IceVial>().Initialize(0, areaSize, finalTarget.position, rotationSpeed, delay, iceDuration);
                break;
        }

    }

    private IEnumerator GetRandomVials(Transform finalTarget)
    {
        int vialCount = Random.Range(minVials, maxVials);
        for (int i = 0; i < vialCount; i++)
        {
            currentVial = (Vial)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Vial)).Length);
            //currentVial = (Vial)values.getValue(Random.Range(0, vialOptions.Length));
            ThrowVial(currentVial, finalTarget);
            yield return new WaitForSeconds(0.3f);
        }

        attackInProgress = false;
    }
}

//vial.GetComponent<VialPrefabScript>().Initialize(finalTarget.position, rotationSpeed, delay, areaSize, lightningDamage, currentVial);
