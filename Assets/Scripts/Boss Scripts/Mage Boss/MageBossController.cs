using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossController : BossController
{
    public int fireballAttackThreshold;
    public int thresholdChange;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (stateMachine.CurrentState() is BossEngagedState && CanAttack())
        {
            EnableAttack();
        }

        if (stateMachine.CurrentState() is BossAttackingState && attackOccured == false)
        {
            DoAttack();
        }

        base.Update();

    }
    protected override void EnableAttack()
    {
        base.EnableAttack();

        if (health.currentHealth < fireballAttackThreshold)
        {
            currentAttack = attacks[1]; // sets to fireball attack
            fireballAttackThreshold -= thresholdChange;
        }


        else if (health.currentHealth < health.maxHealth/2 && attacks[2].GetComponent<MageOrbitalTurretAttack>().turrets.Count < 3)
        {
            currentAttack = attacks[2];
        }

        else
        {
            currentAttack = attacks[0];
        }

        StartAttackWindup();
    }

    private IEnumerator SpawnTurrets()
    {
        yield return new WaitForSeconds(1f);
    }
}

