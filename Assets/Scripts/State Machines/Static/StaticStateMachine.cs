using UnityEngine;

public class StaticStateMachine: MonoBehaviour
{
    protected IStaticState currentState;

    protected virtual void Initialize (IStaticState initialState)
    {
        currentState = initialState;
        currentState?.Enter();
    }

    protected virtual void ChangeState(IStaticState newState)
    {
        if (currentState == newState) return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
