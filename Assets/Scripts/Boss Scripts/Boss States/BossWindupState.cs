using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossWindupState : BossState
{
    public float windupDelay = 0;

    private bool delayStarted;
    public BossWindupState(BossStateMachine stateMachine, GameObject boss) : base(stateMachine, boss) { }

   

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
        stateMachine.ChangeState<BossAttackingState>();
    }

    private void SetVariables(float delay)
    {
        windupDelay = delay;
    }
}
