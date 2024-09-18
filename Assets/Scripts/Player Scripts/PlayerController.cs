using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject boss;
    private PlayerMovement movement;
    private Health health;
    private PlayerStateMachine stateMachine;

    private UIManager uiManager;

    public PlayerAttack rangedAttack;
    public PlayerMeleeAttack meleeAttack;
    public PlayerAttack currentAttack;

    public int healCharges;
    public float healTime;
    public int healAmount;

    public int rangedAmmo;

    public float attackCooldown;
    private float lastAttackTime;

    public bool attackStarted;
    public bool attackExecuted;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<PlayerStateMachine>();
        health = GetComponent<Health>();

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        attackStarted = false;
        attackExecuted = false;

        stateMachine.ChangeState<PlayerEnabledState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Melee Attack
        if (Input.GetMouseButtonDown(0) && CanAttack())
        {
            currentAttack = meleeAttack;
        }
        // Ranged Attack
        if (Input.GetMouseButtonDown(1) && rangedAmmo > 0 && CanAttack())
        {
            currentAttack = rangedAttack;
            rangedAmmo--;
            uiManager.UpdateAmmoCount(rangedAmmo);
        }

        // Heal
        if (Input.GetKeyDown("f") && healCharges > 0)
        {
            StartCoroutine(HealPlayer());
            uiManager.UpdateAmmoCount(healCharges);
        }

        if(Input.GetKeyDown("space") && CanAttack())
        {

        }

        // boss health testing
        if (Input.GetKeyDown("l"))
        {
            boss.GetComponent<Health>().TakeDamage(100);
        }
        //

        if (stateMachine.CurrentState() is PlayerEnabledState && currentAttack != null && (attackStarted == false))
        {
            attackStarted = true;
            currentAttack.StartAttack();
        }
        else if(stateMachine.CurrentState() is PlayerAttackingState && attackExecuted == false)
        {
            attackExecuted = true;
            currentAttack.ExecuteAttack();
            StartCoroutine(AttackReset(currentAttack.followThroughTime));
        }
    }

    private IEnumerator HealPlayer()
    {
        stateMachine.ChangeState<PlayerStunnedState>();
        GetComponent<PlayerStunnedState>().stunDuration = healTime;
        yield return new WaitForSeconds(healTime);
        health.GainHealth(healAmount);
        healCharges--;
    }

    public Vector2 PlayerToMouse()
    {
        Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // gets mouse global position

        Vector2 towardsMouseFromPlayer = (positionMouse - transform.position).normalized;

        return towardsMouseFromPlayer;
    }
    protected bool CanAttack()
    {
        return Time.time >= attackCooldown + lastAttackTime;
    }

    protected IEnumerator AttackReset(float followThrough)
    {
        yield return new WaitForSeconds(followThrough);

        currentAttack.attackInProgress = false;
        currentAttack = null;

        lastAttackTime = Time.time;
        stateMachine.ChangeState<PlayerEnabledState>();
        attackStarted = false;
        attackExecuted = false;

        Debug.Log("ResetToEngaged");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, meleeAttack.meleeRange);
    }
}
