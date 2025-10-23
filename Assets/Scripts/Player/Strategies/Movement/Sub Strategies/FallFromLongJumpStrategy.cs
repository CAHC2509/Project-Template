using UnityEngine;

public class FallFromLongJumpStrategy : FallStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        airSpeed *= player.Data.LongFallAirSpeedMultiplier;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGrounded)
        {
            if (player.Input.DashPressed)
                player.SetStrategy(player.Strategies.Sprint);
            else
                player.SetStrategy(player.Strategies.Idle);
        }
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
}
