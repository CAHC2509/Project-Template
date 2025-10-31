using UnityEngine;

public interface IPlayerMovementController
{
    public IPlayerInputController Input { get; }
    public IPlayerView View { get; }
    public PlayerMovementData MovementData { get; }
    public PlayerMovementStrategies Strategies { get; }
    public Rigidbody2D Rigidbody { get; }
    public Vector2 CurrentVelocity { get; }
    public Vector2 GroundPosition { get; }
    public Vector2 WallPosition { get; }
    public Vector2 LedgePosition { get; }
    public float FacingDirection { get; }
    public bool CanJump { get; }
    public bool CanUseExtraJump { get; }
    public bool CanDash { get; }
    public bool IsFacingRight { get; }
    public bool IsGrounded { get; }
    public bool IsTouchingWall { get; }
    public bool IsTouchingHardSurface { get; }
    public bool CanGrabLedge { get; }

    public void AddForce(Vector2 force, ForceMode2D forceMode);
    public void SetVelocity(Vector2 velocity);
    public void SetVelocityX(float velocity);
    public void SetVelocityY(float velocity);
    public void Flip(float direction);
    public void ConsumeJump();
    public void ResetJump();
}
