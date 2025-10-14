using UnityEngine;

public class LedgeChecker : MonoBehaviour, ILedgeChecker
{
    [Header("Ledge check settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform ledgeCheckOrigin;
    [SerializeField] private float forwardCheckDistance = 0.55f;
    [SerializeField] private float downwardCheckDistance = 0.25f;

    public bool CanGrabLedge { get; private set; }
    public Vector2 LedgePosition { get; private set; }

    private void Update()
    {
        CanGrabLedge = CheckForLedge(out Vector2 ledgePos);

        if (CanGrabLedge)
            LedgePosition = ledgePos;
    }

    private bool CheckForLedge(out Vector2 ledgePos)
    {
        ledgePos = Vector2.zero;

        if (ledgeCheckOrigin == null) return false;

        float direction = Mathf.Sign(transform.localScale.x);
        Vector2 origin = ledgeCheckOrigin.position;
        Vector2 forward = Vector2.right * direction;

        RaycastHit2D forwardHit = Physics2D.Raycast(origin, forward, forwardCheckDistance, obstacleLayer);
        if (forwardHit) return false;

        Vector2 endPoint = origin + forward * forwardCheckDistance;

        RaycastHit2D downHit = Physics2D.Raycast(endPoint, Vector2.down, downwardCheckDistance, groundLayer);
        if (!downHit) return false;

        ledgePos = downHit.point;
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (ledgeCheckOrigin == null) return;

        float direction = Mathf.Sign(transform.localScale.x);
        Vector3 origin = ledgeCheckOrigin.position;
        Vector3 forward = Vector3.right * direction;

        Gizmos.color = CanGrabLedge ? Color.green : Color.red;
        Gizmos.DrawLine(origin, origin + forward * forwardCheckDistance);

        Vector3 forwardEnd = origin + forward * forwardCheckDistance;
        Gizmos.DrawLine(forwardEnd, forwardEnd + Vector3.down * downwardCheckDistance);
    }
}
