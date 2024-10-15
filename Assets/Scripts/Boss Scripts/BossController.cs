using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    protected GameManager gameManager;
    public Health health;
    public BossStateMachine stateMachine;
    public BossMovement bossMovement;

    public BossAttack currentAttack;
    public List<BossAttack> attacks;

    public GameObject corpseSprite;

    public GameObject player;
    public float distanceToPlayer;

    public float lastAttackTime;
    public float attackCooldown;
    public bool attackOccured;

    protected bool meleeDamageOccurred;

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        player = PlayerController.instance.gameObject;

        health = GetComponent<Health>();

        bossMovement = GetComponent<BossMovement>();
        bossMovement.target = player.transform;

        stateMachine = GetComponent<BossStateMachine>();
        stateMachine.ChangeState<BossIdleState>();

        attackOccured = false;
    }

    protected virtual void Update()
    {
        if (stateMachine.CurrentState() is BossEngagedState && CanAttack() && player != null)
        {
            EnableAttack();
        }
        if (stateMachine.CurrentState() is BossAttackingState && attackOccured == false)
        {
            DoAttack();
        }
        if (stateMachine.CurrentState() is BossAttackingState && currentAttack.attackInProgress == false)
        {
            StartCoroutine(AttackReset(currentAttack.followThroughTime));
        }
    }

    protected virtual void EnableAttack()
    {
        distanceToPlayer = (Vector3.Distance(player.transform.position, transform.position));

        // Template

        //if (distanceToPlayer <= Attack1.attackRange)
        //{
        //    Debug.Log("Attack1 Start");
        //    currentAttack = Attack1;
        //
        //    StartAttackWindup();
        //    chosenAttack = Attack.Attack1;
        //}

    }

    protected virtual void DoAttack()
    {
        attackOccured = true;
        
        // Executes the current attack logic
        Debug.Log(currentAttack);
        currentAttack.ExecuteAttack();


    }



    // Calls initial attack start function, which puts boss into windup state, and sets the windup duration
    protected void StartAttackWindup()
    {
        currentAttack.StartAttack(player.transform);
        BossWindupState currentState = GetComponent<BossWindupState>();
        currentState.windupDelay = currentAttack.windupTime;
    }


    // Checks if the boss's cooldowns are completed, and returns a true/false depending.
    protected bool CanAttack()
    {
        return Time.time >= attackCooldown + lastAttackTime;
    }


    // Resets the boss back to its engaged state after an attack, and cleans up variables back to default
    protected IEnumerator AttackReset(float followThrough)
    {
        yield return new WaitForSeconds(followThrough);

        currentAttack.attackInProgress = false;
        lastAttackTime = Time.time;
        stateMachine.ChangeState<BossEngagedState>();
        attackOccured = false;

        Debug.Log("ResetToEngaged");
    }

    

    private void OnDestroy()
    {
        if(health.currentHealth <= 0)
        {
            Instantiate(corpseSprite, transform.position, transform.rotation);
            gameManager.bossDefeated = true;
        }
    }
}
