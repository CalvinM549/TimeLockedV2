using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    // constructor
    public PlayerAttackingState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void EnterState()
    {
        //
        Debug.Log("Player Attacking");
    }

    public override void UpdateState()
    {
        //
    }

    public override void ExitState()
    {
        //
    }
}
