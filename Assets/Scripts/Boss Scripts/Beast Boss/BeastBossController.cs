using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GolemBossController;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        if (stateMachine.CurrentState() is BossEngagedState && CanAttack())
        {
            Debug.Log("1");
            EnableAttack();
        }
        if (stateMachine.CurrentState() is BossAttackingState && attackOccured == false)
        {
            DoAttack();
        }

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
        if (attackOccured)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(meleeAttack.attackDamage);
            }
        }
    }
}
