using UnityEngine;

public class RunStrategy : GroundedStrategy
{
    private float accelerationTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.RUN;
        base.Enter(player);

        accelerationTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.HorizontalInput == 0f)
        {
            (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.RUN_TO_IDLE);
            player.SetStrategy(player.Strategies.Idle);
        }

        if (player.Input.HorizontalInput != 0f && player.Input.HorizontalInput != player.FacingDirection)
        {
            player.View.SetAnimation(Constants.PlayerAnimations.RUNNING_TURN);
            player.Flip(player.Input.HorizontalInput);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.Input.HorizontalInput != 0)
        {
            accelerationTimer += Time.fixedDeltaTime / movementData.RunAccelerationTime;
            accelerationTimer = Mathf.Clamp01(accelerationTimer);
        }
        else
        {
            accelerationTimer = 0f;
        }

        float curveValue = movementData.RunAccelerationCurve.Evaluate(accelerationTimer);
        float velocityX = curveValue * movementData.RunMaxSpeed * player.Input.HorizontalInput;
        player.SetVelocityX(velocityX);
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        player.Input.OnJumplnputPressed += OnJumplnputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnJumplnputPressed -= OnJumplnputPressed;
    }

    private void OnJumplnputPressed() => player.SetStrategy(player.Strategies.Jump);
}
