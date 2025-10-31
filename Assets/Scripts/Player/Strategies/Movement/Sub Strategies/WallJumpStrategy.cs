using UnityEngine;

public class WallJumpStrategy : MovementStrategyBase
{
    private float defaultGravityScale;
    private float elapsedTime;
    private bool allowMovement;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.JUMP;
        collissionData = player.CollissionProfiles.WallJump;
        base.Enter(player);

        defaultGravityScale = player.Rigidbody.gravityScale;
        Vector2 force = new Vector2(movementData.WallJumpForce.x * -player.FacingDirection, movementData.WallJumpForce.y);

        player.Rigidbody.gravityScale = movementData.InAirGravityScale;
        player.SetVelocity(Vector2.zero);
        player.AddForce(force, ForceMode2D.Impulse);
        player.Flip(-player.FacingDirection);

        elapsedTime = 0f;
        allowMovement = false;
    }

    public override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= movementData.WallJumpAirControlDelay && !allowMovement)
            allowMovement = true;

        if (player.CurrentVelocity.y <= Constants.Physics.MIN_FALL_VELOCITY)
            player.SetStrategy(player.Strategies.Fall);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (allowMovement && player.Input.HorizontalInput != 0f)
        {
            player.SetVelocityX(player.Input.HorizontalInput * movementData.AirSpeed);
            player.Flip(player.Input.HorizontalInput);
        }
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        player.Input.OnDashlnputPressed += OnDashInputPressed;
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnDashlnputPressed -= OnDashInputPressed;
        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
    }

    private void OnDashInputPressed()
    {
        if (player.IsGrounded)
            player.SetStrategy(player.Strategies.GroundDash);
        else
            player.SetStrategy(player.Strategies.AirDash);
    }

    private void OnJumpInputPressed()
    {
        if (player.CanUseExtraJump)
            player.SetStrategy(player.Strategies.ExtraJump);
    }

    public override void Exit()
    {
        base.Exit();

        player.Rigidbody.gravityScale = defaultGravityScale;
    }
}
