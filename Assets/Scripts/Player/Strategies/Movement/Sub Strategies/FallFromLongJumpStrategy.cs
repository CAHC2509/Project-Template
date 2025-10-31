using UnityEngine;

public class FallFromLongJumpStrategy : FallStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        airSpeed *= movementData.LongFallAirSpeedMultiplier;
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
        player.SetStrategy(player.Strategies.Fall);
    }

    protected override void HandleGroundTransitions()
    {
        if (!player.IsGrounded) return;

        if (player.Input.DashPressed)
        {
            player.SetStrategy(player.Strategies.Sprint);
            return;
        }

        base.HandleGroundTransitions();
    }
}
