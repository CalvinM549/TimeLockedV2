using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public float LastAttackTime;

    private BossState currentState;

    void Update()
    {
        if (currentState != null) 
        {
            currentState.UpdateState();
        }
    }

    //public void ChangeState(BossState newState)
    public void ChangeState<T>() where T : BossState
    {
        Debug.Log("Changing State...");
        if(currentState != null)
        {
            currentState.ExitState();
            Destroy(currentState);
        }

        currentState = gameObject.AddComponent<T>();
        currentState.Initialize(this);
        currentState.EnterState();
    }

    public BossState CurrentState() { 
        return currentState;
    }

    public void SetLastAttackTime()
    {
        LastAttackTime = Time.time;
    }
}

