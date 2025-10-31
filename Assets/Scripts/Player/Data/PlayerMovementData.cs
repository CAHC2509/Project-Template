using System;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [Header("Run")]
    public float RunMaxSpeed = 5.41f;
    public float RunAccelerationTime = 0f;
    public AnimationCurve RunAccelerationCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

    [Header("Sprint")]
    public AnimationCurve SprintAccelerationCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);
    public float SprintSpeed = 7.6f;
    public float ChangeSprintDirectionTimer = 0.1f;
    public float SprintStunDuration = 0.2f;
    public Vector2 SprintStunForce = new Vector2(6f, 2f);

    [Header("Jump")]
    public float JumpHoldTime = 0.45f;
    public float JumpMultiplier = 10f;
    public float MinJumpHeight = 0.1f;
    public float MaxJumpHeight = 10.5f;
    public float JumpCutMultiplier = 0.5f;

    [Header("Assists")]
    public float CoyoteTime = 0.15f;
    public float JumpBuffer = 0.15f;

    [Header("Extra Jump")]
    public bool HasExtraJump = true;
    public float ExtraJumpForce = 12.5f;

    [Header("Air Movement")]
    public float AirSpeed = 5.41f;
    public float DefaultGravityScale = 1f;
    public float InAirGravityScale = 3f;

    [Header("Falling")]
    public float FallMultiplier = 1.15f;
    public float ShortFallMultiplier = 1.1f;
    public float FallAcceleration = 5f;
    public float MaxFallSpeed = 20f;

    [Header("Long Jump")]
    public float LongJumpAirSpeedMultiplier = 1.5f;

    [Header("Fall from Long Jump")]
    public float LongFallAirSpeedMultiplier = 1.5f;

    [Header("Wall Grab")]
    public float WallGrabDuration = 0.25f;
    public float WallReleaseBuffer = 0.15f;

    [Header("Wall Slide")]
    public float WallSlideMaxSpeed = 8.9f;

    [Header("Wall Run")]
    public float WallRunSpeed = 8.5f;
    public float WallRunDuration = 0.325f;

    [Header("Wall Jump")]
    public Vector2 WallJumpForce = new Vector2(10f, 12.5f);
    public float WallJumpAirControlDelay = 0.2f;

    [Header("Dash")]
    public float DashSpeed = 15f;
    public float DashDuration = 0.25f;
    public float MinDashDuration = 0.15f;

    [Header("Umbrella")]
    public float UmbrellaMaxFallSpeed = 2.5f;
    public float UmbrellaCollissionForce = 2f;
    public float UmbrellaCollisionStunDuration = 0.25f;

    [Header("Ledge Climb")]
    public float LedgeClimbSpeed = 10f;
    public Vector2 LedgeClimbOffset = new Vector2(0.5f, 1f);
}
