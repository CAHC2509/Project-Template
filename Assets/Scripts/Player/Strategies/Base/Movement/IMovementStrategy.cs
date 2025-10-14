using UnityEngine;

public interface IMovementStrategy
{
    public void Enter(PlayerMovementController player);
    public void Update();
    public void FixedUpdate();
    public void Exit();
}
