using UnityEngine;

public class LongJumpStrategy : JumpStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);
        airMovementEnabled = false;

        airSpeed *= movementData.LongJumpAirSpeedMultiplier;
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        player.Input.OnDashlnputCanceled += OnDashInputCanceled;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnDashlnputCanceled -= OnDashInputCanceled;
    }

    private void OnDashInputCanceled()
    {
        TransitionToFall();
    }

    protected override void TransitionToFall()
    {
        float fallMultiplier = shortJump ? movementData.ShortFallMultiplier : movementData.FallMultiplier;
        (player.Strategies.FallFromLongJump as FallStrategy).SetEntryVelocityX(player.CurrentVelocity.x);
        player.SetStrategy(player.Strategies.FallFromLongJump);
        (player.Strategies.FallFromLongJump as FallStrategy).SetFallMultiplier(fallMultiplier);
    }
}
