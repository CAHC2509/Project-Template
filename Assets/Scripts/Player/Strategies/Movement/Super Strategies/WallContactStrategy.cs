using UnityEngine;

public abstract class WallContactStrategy : MovementStrategyBase
{
    private float enterFacingDirection;
    private float releaseBufferTimer;
    private float lastJumpInputTime;
    private bool isInReleaseBuffer;

    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        enterFacingDirection = player.FacingDirection;
        releaseBufferTimer = 0f;
        lastJumpInputTime = -Mathf.Infinity;
        isInReleaseBuffer = false;
    }

    public override void Update()
    {
        base.Update();

        HandleJumpBuffer();
        HandleLedgeGrab();
        HandleWallReleaseBuffer();
        HandleGroundContact();
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

    private void HandleJumpBuffer()
    {
        bool hasBufferedJump = Time.time - lastJumpInputTime <= movementData.JumpBuffer;
        if (hasBufferedJump && player.IsTouchingWall)
            player.SetStrategy(player.Strategies.WallJump);
    }

    private void OnJumpInputPressed()
    {
        lastJumpInputTime = Time.time;

        if (player.IsTouchingWall)
            player.SetStrategy(player.Strategies.WallJump);
    }

    private void HandleLedgeGrab()
    {
        if (player.Input.HorizontalInput != 0f && player.CanGrabLedge)
            player.SetStrategy(player.Strategies.LedgeClimb);
    }

    private void OnDashInputPressed()
    {
        if (player.IsTouchingWall &&  player.Input.HorizontalInput == -enterFacingDirection)
        {
            player.Flip(-enterFacingDirection);
            player.SetStrategy(player.Strategies.Dash);
            return;
        }

        if (player.IsTouchingWall && !player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.WallRun);
            return;
        }
    }

    private void HandleWallReleaseBuffer()
    {
        if (!player.IsTouchingWall && !player.IsGrounded)
        {
            LeftWallAndFall();
            return;
        }

        float input = player.Input.HorizontalInput;
        if (input == 0f)
            return;

        bool isOppositeInput = Mathf.Sign(input) == -player.FacingDirection;

        if (!isInReleaseBuffer)
        {
            if (isOppositeInput && player.IsTouchingWall)
                StartReleaseBuffer();

            return;
        }

        releaseBufferTimer += Time.deltaTime;

        if (!isOppositeInput || (player.IsTouchingWall && Mathf.Sign(input) == player.FacingDirection))
        {
            ResetReleaseBuffer();
            return;
        }

        if (releaseBufferTimer >= movementData.WallReleaseBuffer)
        {
            ResetReleaseBuffer();
            LeftWallAndFall();
            return;
        }
    }

    private void StartReleaseBuffer()
    {
        isInReleaseBuffer = true;
        releaseBufferTimer = 0f;
    }

    private void ResetReleaseBuffer()
    {
        isInReleaseBuffer = false;
        releaseBufferTimer = 0f;
    }

    private void HandleGroundContact()
    {
        if (player.IsGrounded)
        {
            (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.LAND);
            player.SetStrategy(player.Strategies.Idle);
        }
    }

    protected virtual void LeftWallAndFall()
    {
        float input = player.Input.HorizontalInput;
        if (input != 0f && input != enterFacingDirection)
            player.Flip(input);

        player.SetStrategy(player.Strategies.Fall);
    }
}
