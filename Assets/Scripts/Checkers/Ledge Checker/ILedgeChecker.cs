using UnityEngine;

public interface ILedgeChecker
{
    public bool CanGrabLedge { get; }
    public Vector2 LedgePosition { get; }
}
