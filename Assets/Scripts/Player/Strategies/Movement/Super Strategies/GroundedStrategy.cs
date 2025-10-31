using UnityEngine;

public abstract class GroundedStrategy : MovementStrategyBase
{
    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        SnapPlayerToGround();
        player.SetVelocityY(0f);
        player.ResetJump();
        player.ResetExtraJump();
        player.ResetDash();
    }

    public override void Update()
    {
        base.Update();

        if (player.CurrentVelocity.y < Constants.Physics.MIN_FALL_VELOCITY && !player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.Fall);
            return;
        }
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        player.Input.OnDashlnputPressed += OnDashInputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnDashlnputPressed -= OnDashInputPressed;
    }

    public override void Exit()
    {
        base.Exit();

        RemoveListeners();
    }

    protected virtual void OnDashInputPressed()
    {
        if (player.CanDash)
        {
            if (player.IsGrounded)
                player.SetStrategy(player.Strategies.GroundDash);
            else
                player.SetStrategy(player.Strategies.AirDash);
        }
    }
}
