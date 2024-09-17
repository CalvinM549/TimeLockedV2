using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerState
{
    // constructor
    public PlayerStunnedState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }
    public float stunDuration = 0;
    private bool stunStarted = false;
    public float stunDrag;

    public override void EnterState()
    {
        // TODO
        Debug.Log("Stunned!");
        stunDrag = 20;
        stunStarted = false;
    }

    public override void UpdateState()
    {
        if (stunDuration != 0 && !stunStarted)
        {
            StartCoroutine(WaitStunDuration());
        }
    }

    public override void ExitState()
    {
        Debug.Log("ExitStun");
        stunDuration = 0;

        player.GetComponent<Rigidbody2D>().drag -= stunDrag;
    }

    public void SetStun(float stunDur)
    {
        stunDuration = stunDur;
    }

    private IEnumerator WaitStunDuration()
    {
        stunStarted = true;
        player.GetComponent<Rigidbody2D>().drag += stunDrag;
        yield return new WaitForSeconds(stunDuration);
        stateMachine.ChangeState<PlayerEnabledState>();
    }
}
