using UnityEngine;

public class SprintStrategy : GroundedStrategy
{
    private float accelerationTimer;
    private int lastDirection;
    private float idleBufferTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.SPRINT_ANIMATION;
        base.Enter(player);

        accelerationTimer = 1f;
        lastDirection = Mathf.RoundToInt(Mathf.Sign(player.Input.HorizontalInput));
        idleBufferTimer = 0f;
    }

    public override void Update()
    {
        if (player.Input.HorizontalInput == 0f)
            idleBufferTimer += Time.deltaTime;
        else
            idleBufferTimer = 0f;

        if (idleBufferTimer >= player.Data.ChangeSprintDirectionTimer)
        {
            player.SetStrategy(player.Strategies.Idle);
            return;
        }

        if (player.IsTouchingHardSurface)
        {
            player.SetStrategy(player.Strategies.StunFromSprint);
            return;
        }

        if (!player.IsGrounded && player.Input.DashPressed)
        {
            player.SetStrategy(player.Strategies.FallFromLongJump);
            return;
        }

        player.Flip(player.Input.HorizontalInput);

        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        int currentDirection = Mathf.RoundToInt(Mathf.Sign(player.Input.HorizontalInput));

        if (currentDirection != 0 && currentDirection != lastDirection)
        {
            accelerationTimer = 0f;
            lastDirection = currentDirection;
        }

        if (accelerationTimer < 1f)
        {
            accelerationTimer += Time.fixedDeltaTime / player.Data.RunAccelerationTime;
            accelerationTimer = Mathf.Clamp01(accelerationTimer);
        }

        float curveValue = player.Data.SprintAccelerationCurve.Evaluate(accelerationTimer);
        float velocityX = curveValue * player.Data.SprintSpeed * player.Input.HorizontalInput;
        player.SetVelocityX(velocityX);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        player.Input.OnDashlnputCanceled += OnDashInputCanceled;
        player.Input.OnJumplnputPressed += OnJumplnputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        player.Input.OnDashlnputCanceled -= OnDashInputCanceled;
        player.Input.OnJumplnputPressed -= OnJumplnputPressed;
    }

    private void OnDashInputCanceled()
    {
        if (player.Input.HorizontalInput != 0f)
            player.SetStrategy(player.Strategies.Run);
    }

    private void OnJumplnputPressed()
    {
        player.SetStrategy(player.Strategies.LongJump);
    }
}
