using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public float LastAttackTime;

    private PlayerState currentState;

    void Start()
    {

    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    //public void ChangeState(BossState newState)
    public void ChangeState<T>() where T : PlayerState
    {
        Debug.Log("Changing State...");
        if (currentState != null)
        {
            currentState.ExitState();
            Destroy(currentState);
        }

        currentState = gameObject.AddComponent<T>();
        currentState.Initialize(this);
        currentState.EnterState();
    }

    public PlayerState CurrentState()
    {
        return currentState;
    }

    public void SetLastAttackTime()
    {
        LastAttackTime = Time.time;
    }
}

