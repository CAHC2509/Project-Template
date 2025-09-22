using System;
using UnityEngine;

public abstract class GameStateBase : MonoBehaviour
{
    public Action<States> FinishState;

    protected States nextState;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        GameManager.SetState?.Invoke(this);
    }

    protected virtual void EnterState() { }
    
    protected virtual void ExitState()
    {
        FinishState?.Invoke(nextState);
    }
}
