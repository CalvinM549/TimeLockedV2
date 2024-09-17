using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    protected BossStateMachine stateMachine;
    protected GameObject boss;

    protected BossState(BossStateMachine stateMachine, GameObject boss)
    {
        this.stateMachine = stateMachine;
        this.boss = boss;
    }

    public void Initialize(BossStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.boss = stateMachine.gameObject;
    }
    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void UpdateState();


}
