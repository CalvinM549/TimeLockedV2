using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnabledState : PlayerState
{
    // constructor
    public PlayerEnabledState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    private PlayerMovement movement;

    public override void EnterState()
    {
        movement = GetComponent<PlayerMovement>();
        movement.movementEnabled = true;
    }

    public override void UpdateState()
    {
        //
    }

    public override void ExitState()
    {
        movement.movementEnabled = false;
    }
}
