using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastBossController : BossController
{
    public BossAttack meleeAttack;
    public BossAttack spineAttack;
    public BossAttack shockwaveAttack;

    public float playerProximityTimer;
    public float lastShockwave;
    public float shockwaveCooldown;

    public bool phaseChanged;
    public float phaseTwoCooldown;
    
    private int attackCount;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        meleeDamageOccurred = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (health.currentHealth <= health.maxHealth/2 && !phaseChanged)
        {
            PhaseChange();
        }

    }

    private void FixedUpdate()
    {
        PlayerProximityUpdate();

    }

    protected override void EnableAttack()
    {
        Debug.Log("Attacks Enabled");
        base.EnableAttack();

        // Attack Selection Logic

        meleeDamageOccurred = false;
        
        if (CanShockwave())
        {
            Debug.Log("ShockwaveAttack");
            currentAttack = shockwaveAttack;
            lastShockwave = Time.time;
        }
        else
        {
            switch (attackCount)
            {
                case 0:
                    currentAttack = meleeAttack;
                    break;
                case 1:
                    currentAttack = meleeAttack;
                    break;
                case 2:
                    currentAttack = spineAttack;
                    break;
            }
            if (attackCount == 2)
            {
                attackCount = 0;
            }
            else
            {
                attackCount++;
            }
        }
        

        StartAttackWindup();
    }

    private void PhaseChange()
    {
        attackCooldown = phaseTwoCooldown;
        meleeAttack.followThroughTime = 0.2f;
        phaseChanged = true;
    }

    private bool CanShockwave()
    {
        return playerProximityTimer >= shockwaveCooldown + lastShockwave;
    }

    private void PlayerProximityUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, shockwaveAttack.attackRange);
        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerProximityTimer += Time.deltaTime;
            }
        }
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
