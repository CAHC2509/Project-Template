using UnityEngine;

public class PlayerMovementController : PlayerComponentBase, IPlayerMovementController
{
    [field: SerializeField] public PlayerDataEntity Data { get; private set; }

    public IPlayerInputController Input { get; private set; }
    public IPlayerView View { get; private set; }
    public PlayerMovementStrategies Strategies { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 LedgePosition => ledgeChecker.LedgePosition;
    public float FacingDirection { get; private set; }
    public bool CanDash { get; private set; }
    public bool CanJump { get; private set; }
    public bool CanUseExtraJump { get; private set; }
    public bool IsFacingRight { get; private set; }
    public bool IsGrounded => groundChecker.IsGrounded;
    public bool IsTouchingWall => wallChecker.IsTouchingWall;
    public bool IsTouchingHardSurface => hardSurfaceChecker.IsTouchingHardSurface;
    public bool CanGrabLedge => ledgeChecker.CanGrabLedge;

    private IGroundChecker groundChecker;
    private IWallChecker wallChecker;
    private ILedgeChecker ledgeChecker;
    private IHardSurfaceChecker hardSurfaceChecker;
    private IMovementStrategy currentStrategy;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<IGroundChecker>();
        wallChecker = GetComponent<IWallChecker>();
        ledgeChecker = GetComponent<ILedgeChecker>();
        hardSurfaceChecker = GetComponent<IHardSurfaceChecker>();
        View = GetComponentInChildren<IPlayerView>();
        Input = GetComponent<IPlayerInputController>();
    }

    public override void Initialize()
    {
        Strategies = new PlayerMovementStrategies();

        SetStrategy(Strategies.Idle);
        currentStrategy.Enter(this);

        ResetJump();
        ResetDash();
        IsFacingRight = true;
        FacingDirection = 1f;
    }

    public override void Conclude()
    {
        currentStrategy.Exit();
    }

    private void Update()
    {
        currentStrategy?.Update();
    }

    private void FixedUpdate()
    {
        currentStrategy?.FixedUpdate();
        CurrentVelocity = rb.linearVelocity;
    }

    public void SetStrategy(IMovementStrategy newStrategy)
    {
        if (currentStrategy == newStrategy)
            return;

        Debug.Log($"Current: {currentStrategy} - New: {newStrategy}");

        currentStrategy?.Exit();
        currentStrategy = newStrategy;
        currentStrategy.Enter(this);
    }

    public void Flip(float direction)
    {
        if (direction == 0f) return;

        bool shouldFaceRight = direction > 0f;

        if (IsFacingRight != shouldFaceRight)
        {
            IsFacingRight = shouldFaceRight;
            FacingDirection = IsFacingRight ? 1f : -1f;

            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x) * FacingDirection, currentScale.y, currentScale.z);
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
        CurrentVelocity = velocity;
    }

    public void SetVelocityX(float velocity)
    {
        rb.linearVelocityX = velocity;
        CurrentVelocity = new Vector2(velocity, CurrentVelocity.y);
    }

    public void SetVelocityY(float velocity)
    {
        rb.linearVelocityY = velocity;
        CurrentVelocity = new Vector2(CurrentVelocity.x, velocity);
    }

    public void AddForce(Vector2 force, ForceMode2D forceMode) => rb.AddForce(force, forceMode);
    public void ConsumeJump() => CanJump = false;
    public void ConsumeExtraJump() => CanUseExtraJump = false;
    public void ConsumeDash() => CanDash = false;
    public void ResetJump() => CanJump = true;
    public void ResetExtraJump() => CanUseExtraJump = Data.HasExtraJump;
    public void ResetDash() => CanDash = true;
}
