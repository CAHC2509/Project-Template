using UnityEngine;

public abstract class MovementStrategyBase : IMovementStrategy
{
    protected PlayerMovementController player;
    protected PlayerMovementData movementData;
    protected string animationName = string.Empty;
    protected string entryAnimationName = string.Empty;

    public virtual void Enter(PlayerMovementController player)
    {
        this.player = player;
        movementData = player.MovementData;
        
        if (entryAnimationName != string.Empty)
            player.View.SetAnimation(entryAnimationName);
        else
            player.View.SetAnimation(animationName);

        AddListeners();
    }

    public virtual void Exit()
    {
        entryAnimationName = string.Empty;
        RemoveListeners();
    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }

    public void SetEntryAnimation(string entryAnimationName)
    {
        this.entryAnimationName = entryAnimationName;
    }
}
