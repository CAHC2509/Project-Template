using UnityEngine;

public class GroundedStrategy : MovementStrategyBase
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        player.ResetJump();
        player.ResetExtraJump();
        player.ResetDash();
    }

    public override void Update()
    {
        base.Update();

        if (player.CurrentVelocity.y < Constants.Player.MIN_FALL_VELOCITY && !player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.Fall);
            return;
        }
    }

    protected override void AddListeners()
    {
        player.Input.OnDashlnputPressed += OnDashInputPressed;
    }

    protected override void RemoveListeners()
    {
        player.Input.OnDashlnputPressed -= OnDashInputPressed;
    }

    protected virtual void OnDashInputPressed()
    {
        if (player.CanDash)
            player.SetStrategy(player.Strategies.Dash);
    }
}
