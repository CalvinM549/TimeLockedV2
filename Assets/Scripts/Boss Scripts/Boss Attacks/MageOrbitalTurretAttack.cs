using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageOrbitalTurretAttack : BossAttack
{
    public GameObject turretPrefab;

    private Transform target;
    
    
    public List<GameObject> turrets = new List<GameObject>();

    public int[] turretDistance;

    public override void StartAttack(Transform target)
    {
        this.target = target;
        Debug.Log("StartAttack");

        base.StartAttack(target);
  
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;

        Debug.Log(turrets.Count);
        turrets.Add(Instantiate(turretPrefab, transform.position, transform.rotation) as GameObject);

        Debug.Log(turrets.Count);
        turrets[turrets.Count-1].GetComponent<OrbitingObjectScript>().Initialize(target, turretDistance[turrets.Count-1]);


        attackInProgress = false;
    }
}
