using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindupState : PlayerState
{
    // constructor
    public PlayerWindupState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public float windupDelay;
    public bool delayStarted;

    public override void EnterState()
    {
        Debug.Log("Enter Windup");
        delayStarted = false;
    }

    public override void UpdateState()
    {
        if (delayStarted == false)
        {
            delayStarted = true;
            StartCoroutine(DelayAttack(windupDelay));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Windup");
    }

    private IEnumerator DelayAttack(float delayAmount)
    {
        yield return new WaitForSeconds(delayAmount);
        stateMachine.ChangeState<PlayerAttackingState>();
    }

}