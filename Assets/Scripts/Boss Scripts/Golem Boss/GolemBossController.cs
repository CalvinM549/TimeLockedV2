using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// contains logic for all boss actions

public class GolemBossController : BossController
{
    private BossAttack meleeAttack;
    private BossAttack vialAttack;
    private BossAttack burstAttack;

    public int powerAttackThreshold;
    public int thresholdChange;




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        meleeDamageOccurred = false;
        meleeAttack = attacks[0];
        vialAttack = attacks[1];
        burstAttack = attacks[2];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnableAttack()
    {
        base.EnableAttack();

        // Attack Selection Logic
        meleeDamageOccurred = false;

        if (health.currentHealth < powerAttackThreshold)
        {
            Debug.Log("BurstAttack");
            currentAttack = burstAttack;
            powerAttackThreshold -= thresholdChange;
        }

        else if (distanceToPlayer <= meleeAttack.attackRange)
        {
            Debug.Log("MeleeAttackStart");
            currentAttack = meleeAttack;
        }

        else
        {
            Debug.Log("VialAttackStart");
            currentAttack = vialAttack;
        }

        StartAttackWindup();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attackOccured && meleeDamageOccurred == false)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(meleeAttack.attackDamage);
            }
            meleeDamageOccurred = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (attackOccured && meleeDamageOccurred == false)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(meleeAttack.attackDamage);
            }
            meleeDamageOccurred = true;
        }
    }

}
