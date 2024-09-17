using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;
    protected GameObject player;

    protected PlayerState(PlayerStateMachine stateMachine, GameObject player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    public void Initialize(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.player = stateMachine.gameObject;
    }
    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void UpdateState();


}

// Player States list
// 
// Enabled - allows inputs
// Idle?
// Windup
// Attacking
// Stunned

