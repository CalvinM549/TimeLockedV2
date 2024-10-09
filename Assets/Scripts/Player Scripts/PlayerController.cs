using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

             private Health health;
    private PlayerStateMachine stateMachine;

    private UIManager uiManager;

    public PlayerAttack rangedAttack;
    public PlayerMeleeAttack meleeAttack;
    public PlayerAttack currentAttack;



    public int healCharges;
    public float healTime;
    public int healAmount;

    public bool healOccuring;

    public int rangedAmmo;

    public float attackCooldown;
    private float lastAttackTime;

    public bool attackStarted;
    public bool attackExecuted;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
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
            uiManager.UpdateAmmoCount(rangedAmmo);
        }
        // Ranged Attack
        if (Input.GetMouseButtonDown(1) && rangedAmmo > 0 && CanAttack())
        {
            currentAttack = rangedAttack;
            rangedAmmo--;
            uiManager.UpdateAmmoCount(rangedAmmo);
        }

        // Heal
        if (Input.GetKeyDown("f") && healCharges > 0 && !healOccuring)
        {
            StartCoroutine(HealPlayer());
        }


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
        healOccuring = true;
        stateMachine.ChangeState<PlayerStunnedState>();
        GetComponent<PlayerStunnedState>().stunDuration = healTime;
        yield return new WaitForSeconds(healTime);
        health.GainHealth(healAmount);
        healCharges--;

        uiManager.UpdateHealCharges(healCharges);
        healOccuring = false;
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
