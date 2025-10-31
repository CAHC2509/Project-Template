using UnityEngine;

public abstract class MovementStrategyBase : IMovementStrategy
{
    protected PlayerMovementController player;
    protected PlayerMovementData movementData;
    protected PlayerCollissionData collissionData;

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

        if (collissionData != null)
            player.CollissionAdjuster.AdjustCollisions(collissionData);

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

    protected void SnapPlayerToGround()
    {
        Vector2 groundPosition = player.GroundPosition;
        Vector2 playerPosition = player.transform.position;
        float targetY = groundPosition.y - collissionData.colliderOffset.y + (collissionData.colliderSize.y * 0.5f);

        player.transform.position = new Vector2(playerPosition.x, targetY);
    }
}
