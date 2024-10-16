using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    private BossStateMachine stateMachine;
    protected Animator anim;

    public bool attackInProgress;

    public int attackDamage;
    public float attackRange;

    public float windupTime;
    public float followThroughTime;
    

    public virtual void StartAttack(Transform target)
    {
        stateMachine = GetComponent<BossStateMachine>();
        stateMachine.ChangeState<BossWindupState>();
        anim = GetComponent<Animator>();
    }

    public abstract void ExecuteAttack();

}
