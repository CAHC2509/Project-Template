using UnityEngine;

public class LongJumpStrategy : JumpStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);
    }

    protected override void ApplyInitialJumpSettings()
    {
        initialJumpForceToUse = player.Data.InitialJumpForce * player.Data.LongJumpForceMultiplier;
        maxJumpForceToUse = player.Data.MaxJumpForce * player.Data.MaxLongJumpForceMultiplier;
        airSpeedToUse = player.Data.AirSpeed;
    }

    protected override void TransitionToFall()
    {
        player.SetStrategy(player.Strategies.FallFromLongJump);
    }
}
