using UnityEngine;

public class WallSlideStrategy : MovementStrategyBase
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.WALL_SLIDE_ANIMATION;
        base.Enter(player);

        player.SetVelocity(Vector2.zero);
        player.ResetJump();
        player.ResetExtraJump();
        player.ResetDash();
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.HorizontalInput == 0f || !player.IsTouchingWall || player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.Fall);
            return;
        }

        if (player.Input.HorizontalInput != 0f && player.CanGrabLedge)
        {
            player.SetStrategy(player.Strategies.LedgeClimb);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityX(0f);
        player.SetVelocityY(-player.Data.WallSlideSpeed);
    }

    protected override void AddListeners()
    {
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
    }

    protected override void RemoveListeners()
    {
        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
    }

    private void OnJumpInputPressed()
    {
        player.SetStrategy(player.Strategies.WallJump);
    }
}
