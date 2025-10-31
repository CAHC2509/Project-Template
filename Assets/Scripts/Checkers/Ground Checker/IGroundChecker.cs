using UnityEngine;

public interface IGroundChecker
{
    public Vector2 GroundPosition { get; }
    public bool IsGrounded { get; }
}
