using System;
using UnityEngine;

[Serializable]
public class PlayerDataEntity
{
    [Header("Run")]
    public float RunMaxSpeed = 5f;
    public float RunAccelerationTime = 0.5f;
    public AnimationCurve RunAccelerationCurve = AnimationCurve.EaseInOut(0f, 0.85f, 1f, 1f);

    [Header("Sprint")]
    public AnimationCurve SprintAccelerationCurve = AnimationCurve.EaseInOut(0f, 0.85f, 1f, 1f);
    public float SprintSpeed = 7.5f;
    public float ChangeSprintDirectionTimer = 0.1f;
    public float SprintStunDuration = 0.25f;
    public Vector2 SprintStunForce = new Vector2(2.5f, 7.5f);

    [Header("Jump")]
    public float JumpHoldTime = 0.25f;
    public float JumpMultiplier = 0.25f;
    public float MinJumpHeight = 0.25f;
    public float MaxJumpHeight = 0.25f;
    public float JumpCutMultiplier = 0.5f;

    [Header("Assists")]
    public float CoyoteTime = 0.3f;
    public float JumpBuffer = 0.15f;

    [Header("Extra Jump")]
    public bool HasExtraJump = true;
    public float ExtraJumpForce = 10f;

    [Header("Air Movement")]
    public float AirSpeed = 5f;
    public float DefaultGravityScale = 1f;
    public float InAirGravityScale = 3f;

    [Header("Falling")]
    public float FallMultiplier = 0.25f;
    public float ShortFallMultiplier = 0.25f;
    public float FallAcceleration = 30f;
    public float MaxFallSpeed = 20f;

    [Header("Long Jump")]
    public float LongJumpAirSpeedMultiplier = 1.5f;

    [Header("Fall from Long Jump")]
    public float LongFallAirSpeedMultiplier = 1.5f;

    [Header("Wall Grab")]
    public float WallGrabDuration = 0.1f;
    public float WallReleaseBuffer = 0.1f;

    [Header("Wall Slide")]
    public float WallSlideMaxSpeed = 8f;

    [Header("Wall Run")]
    public float WallRunSpeed = 9f;
    public float WallRunDuration = 0.45f;

    [Header("Wall Jump")]
    public Vector2 WallJumpForce = new Vector2(5f, 5f);
    public float WallJumpAirControlDelay = 0.1f;

    [Header("Dash")]
    public float DashSpeed = 12.5f;
    public float DashDuration = 0.75f;

    [Header("Umbrella")]
    public float UmbrellaMaxFallSpeed = 2.5f;
    public float UmbrellaCollissionForce = 5f;
    public float UmbrellaCollisionStunDuration = 0.25f;

    [Header("Ledge Climb")]
    public float LedgeClimbSpeed = 0.2f;
    public Vector2 LedgeClimbOffset = new Vector2(0.5f, 1f);
}
