using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEngagedState : BossState
{
    public BossEngagedState(BossStateMachine stateMachine, GameObject boss) : base(stateMachine, boss) { }

    public BossMovement bossMovement;
    public GameObject player;

    public override void EnterState()
    {
        player = GameObject.Find("Player");

        bossMovement = GetComponent<BossMovement>();
        bossMovement.movementEnabled = true;
        anim.SetBool("isMoving", true); 

    }

    public override void UpdateState()
    {
        //
    }

    public override void ExitState()
    {
        bossMovement.movementEnabled = false;
        anim.SetBool("isMoving", false);
        Debug.Log("Exit Engaged");
    }
}
