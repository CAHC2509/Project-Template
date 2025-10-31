using UnityEngine;

public abstract class DashBaseStrategy : MovementStrategyBase
{
    protected float dashTimer;

    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        dashTimer = 0f;
        player.ConsumeDash();
    }

    public override void Update()
    {
        base.Update();
        dashTimer += Time.deltaTime;

        if (dashTimer > movementData.DashDuration)
            HandleDashEnd();
    }

    public override void Exit()
    {
        base.Exit();

        RemoveListeners();
    }

    protected void TransitionToFallFromLongJump()
    {
        float finalVelocity = movementData.AirSpeed * movementData.LongFallAirSpeedMultiplier * player.FacingDirection;
        (player.Strategies.FallFromLongJump as FallFromLongJumpStrategy).SetEntryVelocityX(finalVelocity);
        (player.Strategies.FallFromLongJump as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.ROLLING_FALL);
        player.SetStrategy(player.Strategies.FallFromLongJump);
    }

    protected abstract void HandleDashEnd();
}
