using System.Collections.Generic;
using UnityEngine;
using MEC;

public class DistanceMeter : MonoBehaviour
{
    [SerializeField] private float countdownTime = 1.85f;
    [SerializeField] private bool activeMeter = false;

    private IPlayerInputController inputController;
    private PlayerMovementController movementController;
    private Rigidbody2D rb2d;
    private Vector2 startPosition;

    private void Awake()
    {
        inputController = GetComponent<IPlayerInputController>();
        movementController = GetComponent<PlayerMovementController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputController.OnHorizontalInput += StartDistanceMedition;
    }

    private void OnDisable()
    {
        inputController.OnHorizontalInput -= StartDistanceMedition;
    }

    private void StartDistanceMedition(float input)
    {
        if (!activeMeter) return;

        startPosition = transform.position;
        Timing.RunCoroutine(DistanceCoroutine());
    }

    private IEnumerator<float> DistanceCoroutine()
    {
        yield return Timing.WaitForSeconds(countdownTime);

        rb2d.linearVelocityX = 0f;
        movementController.enabled = false;

        float distance = Mathf.Abs(startPosition.x - transform.position.x);
        Debug.Log($"Distance: {distance:F3}");
    }
}
