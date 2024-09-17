using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// contains logic for all boss actions

public class GolemBossController : BossController
{

    public BossAttack meleeAttack;
    public BossAttack vialAttack;
    public BossAttack burstAttack;

    private bool hasPowerAttacked;

    public int powerAttackThreshold;




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.CurrentState() is BossEngagedState && CanAttack())
        {
            EnableAttack();
        }
        if (stateMachine.CurrentState() is BossAttackingState && attackOccured == false)
        {
            DoAttack();
        }

        if (health.currentHealth%powerAttackThreshold == 0 && !hasPowerAttacked)
        {
            hasPowerAttacked = true;
            DoPowerAttack();
        }
        else if (!(health.currentHealth % powerAttackThreshold == 0)){
            hasPowerAttacked = false;
        }
    }

    protected override void EnableAttack()
    {
        base.EnableAttack();

        // Attack Selection Logic

        if (distanceToPlayer <= meleeAttack.attackRange)
        {
            Debug.Log("MeleeAttackStart");
            currentAttack = meleeAttack;

            StartAttackWindup();
        }
        else if (distanceToPlayer <= vialAttack.attackRange)
        {
            Debug.Log("VialAttackStart");
            currentAttack = vialAttack;

            StartAttackWindup();
        }        
    }

    private void DoPowerAttack()
    {
        Debug.Log("BurstAttack");
        currentAttack = burstAttack;

        StartAttackWindup();
    }
}
