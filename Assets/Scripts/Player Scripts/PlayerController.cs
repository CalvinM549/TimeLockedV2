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

    public PlayerAttack rangedAttack;
    public PlayerAttack meleeAttack;
    public PlayerAttack currentAttack;

    public int healCharges;
    public float healTime;
    public int healAmount;

    public int rangedAmmo;

    public float attackCooldown;
    private float lastAttackTime;

    public bool attackOccurred;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<PlayerStateMachine>();
        health = GetComponent<Health>();

        attackOccurred = false;

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
        }

        // Heal
        if (Input.GetKeyDown("f") && healCharges > 0)
        {
            StartCoroutine(HealPlayer());
        }

        if(Input.GetKeyDown("space") && CanAttack())
        {

        }

        if (stateMachine.CurrentState() is PlayerEnabledState && currentAttack != null && (!attackOccurred))
        {
            attackOccurred = true;
            currentAttack.StartAttack();
        }
        else if(stateMachine.CurrentState() is PlayerAttackingState && attackOccurred)
        {
            currentAttack.ExecuteAttack();
            AttackReset(currentAttack.followThroughTime);
        }
    }

    private IEnumerator HealPlayer()
    {
        stateMachine.ChangeState<PlayerStunnedState>();
        GetComponent<PlayerStunnedState>().stunDuration = healTime;
        yield return new WaitForSeconds(healTime);
        health.GainHealth(healAmount);
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
        lastAttackTime = Time.time;
        stateMachine.ChangeState<PlayerEnabledState>();
        attackOccurred = false;

        Debug.Log("ResetToEngaged");
    }
}
