using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackingState : BossState
{
    public BossAttackingState(BossStateMachine stateMachine, GameObject boss)
        : base(stateMachine, boss) { }
    
    public override void EnterState()
    {
        Debug.Log("Enter Attacking");
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        Debug.Log("Exit Attacking");
    }
}