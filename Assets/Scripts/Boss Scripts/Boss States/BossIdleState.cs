using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(BossStateMachine stateMachine, GameObject boss) : base(stateMachine, boss) { }

    public bool stateChangeNotAttempted = true;

    public override void EnterState()
    {
        //
    }

    public override void UpdateState()
    {
        StartCoroutine(DelayedUpdate());
        if (stateChangeNotAttempted)
        {
            stateChangeNotAttempted = false;
        }
    }

    public override void ExitState()
    {
        //
    }

    public IEnumerator DelayedUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        stateMachine.ChangeState<BossEngagedState>();
    }


}
