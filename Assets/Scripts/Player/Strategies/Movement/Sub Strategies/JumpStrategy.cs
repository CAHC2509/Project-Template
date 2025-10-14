using UnityEngine;

public class JumpStrategy : InAirStrategy
{
    protected float initialJumpForceToUse;
    protected float maxJumpForceToUse;

    private float coyoteTimer;
    private float jumpHoldTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.JUMP_ANIMATION;
        base.Enter(player);

        ApplyInitialJumpSettings();
        coyoteTimer = 0f;
        jumpHoldTimer = 0f;

        player.SetVelocityY(0f);
        player.AddForce(Vector2.up * initialJumpForceToUse, ForceMode2D.Impulse);
        player.ConsumeJump();
    }

    public override void Update()
    {
        base.Update();

        coyoteTimer += Time.deltaTime;

        if (player.Input.JumpPressed)
            jumpHoldTimer += Time.deltaTime;

        if (player.CurrentVelocity.y < Constants.Player.MIN_FALL_VELOCITY)
        {
            TransitionToFall();
            return;
        }

        if (player.Input.HorizontalInput != 0f && player.CanGrabLedge)
        {
            player.SetStrategy(player.Strategies.LedgeClimb);
            return;
        }

        if (player.IsGrounded && coyoteTimer > player.Data.CoyoteTime)
        {
            player.SetStrategy(player.Strategies.Idle);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.Input.JumpPressed)
            ApplyCurveJumpForce();
        else
            ReduceRisingForce();
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
        if (player.CanUseExtraJump)
            player.SetStrategy(player.Strategies.ExtraJump);
    }

    protected virtual void ApplyInitialJumpSettings()
    {
        initialJumpForceToUse = player.Data.InitialJumpForce;
        maxJumpForceToUse = player.Data.MaxJumpForce;
        airSpeedToUse = player.Data.AirSpeed;
    }

    protected virtual void TransitionToFall()
    {
        player.SetStrategy(player.Strategies.Fall);
    }

    private void ApplyCurveJumpForce()
    {
        float time = jumpHoldTimer / player.Data.MaxJumpHoldTime;
        float curveValue = player.Data.JumpForceCurve.Evaluate(time);

        if (time <= 1f)
        {
            float force = curveValue * maxJumpForceToUse;
            player.AddForce(Vector2.up * force, ForceMode2D.Force);
        }
    }

    private void ReduceRisingForce() => player.AddForce(Vector2.down * player.Data.FallAcceleration, ForceMode2D.Force);
}
