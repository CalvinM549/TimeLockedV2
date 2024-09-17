using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    private PlayerStateMachine stateMachine;

    public bool attackInProgress;

    public int attackDamage;

    public float windupTime;
    public float followThroughTime;

    public virtual void StartAttack()
    {

        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.ChangeState<PlayerWindupState>();

        PlayerWindupState windupState = GetComponent<PlayerWindupState>();
        windupState.windupDelay = windupTime;
    }

    public abstract void ExecuteAttack();
}
