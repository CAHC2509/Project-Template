using UnityEngine;

public class AirTimeMeter : MonoBehaviour
{
    private IGroundChecker groundChecker;
    private Rigidbody2D rb;

    private float leaveGroundTime;
    private float peakTime;
    private float ascendDuration;
    private float descendDuration;
    private bool previousGrounded;
    private bool hasReachedPeak;

    private void Awake()
    {
        groundChecker = GetComponent<IGroundChecker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isGrounded = groundChecker.IsGrounded;

        if (previousGrounded && !isGrounded)
        {
            leaveGroundTime = Time.time;
            hasReachedPeak = false;
            ascendDuration = 0f;
            descendDuration = 0f;
        }

        if (!isGrounded && !hasReachedPeak && rb.linearVelocity.y <= 0f)
        {
            peakTime = Time.time;
            ascendDuration = peakTime - leaveGroundTime;
            hasReachedPeak = true;
        }

        if (!previousGrounded && isGrounded)
        {
            float airTime = Time.time - leaveGroundTime;

            if (hasReachedPeak)
                descendDuration = Time.time - peakTime;

            Debug.Log($"Tiempo total en el aire: {airTime:F2}s (Subida: {ascendDuration:F2}s, Bajada: {descendDuration:F2}s)");
        }

        previousGrounded = isGrounded;
    }
}
