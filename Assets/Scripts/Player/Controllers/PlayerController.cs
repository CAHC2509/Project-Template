using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputController inputController;
    private PlayerMovementController movementController;

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        movementController = GetComponent<PlayerMovementController>();
    }

    private void Start()
    {
        inputController.Initialize();
        movementController.Initialize();
    }

    private void OnApplicationQuit()
    {
        inputController.Conclude();
        movementController.Conclude();
    }
}
