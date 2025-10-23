using UnityEngine;

public class JumpStrategy : InAirStrategy
{
    protected bool shortJump;

    private Rigidbody2D rb;
    private float gravityScaleDefault;
    private float jumpForce;
    private float jumpHoldTimer;
    private float jumpStartY;
    private bool jumpCutApplied;
    private bool isHoldingJump;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.JUMP;
        base.Enter(player);

        rb = player.Rigidbody;
        rb.gravityScale = player.Data.InAirGravityScale;
        
        gravityScaleDefault = player.Data.DefaultGravityScale;
        jumpStartY = player.transform.position.y;
        jumpHoldTimer = 0f;
        jumpCutApplied = false;
        isHoldingJump = true;
        shortJump = false;

        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScaleDefault);
        jumpForce = Mathf.Sqrt(2f * gravity * player.Data.MaxJumpHeight);

        player.SetVelocityY(jumpForce);
        player.ConsumeJump();
    }

    public override void Update()
    {
        base.Update();

        HandleJumpHoldRelease();
        HandleJumpCut();
        HandleFallOrLanding();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        ApplyJumpHoldForce();
        ClampMaxJumpHeight();
    }

    private void HandleJumpHoldRelease()
    {
        if (!player.Input.JumpPressed)
            isHoldingJump = false;
    }

    private void HandleJumpCut()
    {
        bool isRising = rb.linearVelocity.y > 0f;

        if (isRising && !isHoldingJump && !jumpCutApplied)
        {
            float newVelocityY = rb.linearVelocity.y * player.Data.JumpCutMultiplier;
            player.SetVelocityY(newVelocityY);

            shortJump = true;
            jumpCutApplied = true;
        }
    }

    private void HandleFallOrLanding()
    {
        bool isDescending = rb.linearVelocity.y <= 0f;

        if (!isDescending)
            return;

        if (player.IsGrounded)
            player.SetStrategy(player.Strategies.Idle);
        else
            TransitionToFall();
    }

    private void ApplyJumpHoldForce()
    {
        bool isRising = rb.linearVelocity.y > 0f;
        if (!isRising || !isHoldingJump || jumpCutApplied)
            return;

        jumpHoldTimer += Time.fixedDeltaTime;

        if (jumpHoldTimer >= player.Data.JumpHoldTime)
        {
            isHoldingJump = false;
            return;
        }

        float progress = jumpHoldTimer / player.Data.JumpHoldTime;
        float currentMultiplier = (progress > 0.5f)
            ? player.Data.JumpMultiplier * (1 - progress)
            : player.Data.JumpMultiplier;

        float newVelocityY = rb.linearVelocity.y + currentMultiplier * Time.fixedDeltaTime;
        player.SetVelocityY(newVelocityY);
    }

    protected virtual void TransitionToFall()
    {
        float fallMultiplier = shortJump ? player.Data.ShortFallMultiplier : player.Data.FallMultiplier;
        player.SetStrategy(player.Strategies.Fall);
        (player.Strategies.Fall as FallStrategy).SetFallMultiplier(fallMultiplier);
    }

    private void ClampMaxJumpHeight()
    {
        float currentHeight = player.transform.position.y - jumpStartY;
        if (currentHeight >= player.Data.MaxJumpHeight && rb.linearVelocity.y > 0f)
            player.SetVelocityY(0f);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
        player.Input.OnDashlnputPressed += OnDashInputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
        player.Input.OnDashlnputPressed -= OnDashInputPressed;
    }

    private void OnJumpInputPressed()
    {
        if (player.CanUseExtraJump)
            player.SetStrategy(player.Strategies.ExtraJump);
    }

    private void OnDashInputPressed()
    {
        if (player.CanDash)
            player.SetStrategy(player.Strategies.Dash);
    }
}
