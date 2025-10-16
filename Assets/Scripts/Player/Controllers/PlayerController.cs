using UnityEngine;

public class PlayerController : ControllerBase
{
    private PlayerInputController inputController;
    private PlayerMovementController movementController;

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        movementController = GetComponent<PlayerMovementController>();
    }

    public override void Initialize()
    {
        base.Initialize();

        inputController.Initialize();
        movementController.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        inputController.Conclude();
        movementController.Conclude();
    }
}
