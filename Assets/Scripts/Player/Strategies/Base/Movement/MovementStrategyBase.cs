using UnityEngine;

public abstract class MovementStrategyBase : IMovementStrategy
{
    protected PlayerMovementController player;
    protected string animationName = string.Empty;

    public virtual void Enter(PlayerMovementController player)
    {
        this.player = player;
        player.View.UpdateAnimation(animationName);
        AddListeners();
    }

    public virtual void Exit()
    {
        RemoveListeners();
    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
