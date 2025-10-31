using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCollissionAdjuster : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform hardSurfaceCheck;
    [SerializeField] private Transform ledgeCheck;

    private BoxCollider2D boxCollider;
    private PlayerCollissionData collissionData;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void AdjustCollisions(PlayerCollissionData collissionData)
    {
        this.collissionData = collissionData;
        boxCollider.offset = collissionData.colliderOffset;
        boxCollider.size = collissionData.colliderSize;

        AdjustGroundCheck();
        AdjustEdgeChecks();
    }

    private void AdjustGroundCheck()
    {
        Vector2 bottomPosition = new Vector2(0f, -collissionData.colliderSize.y * 0.5f + collissionData.colliderOffset.y);
        groundCheck.localPosition = bottomPosition;
    }

    private void AdjustEdgeChecks()
    {
        float rightEdge = collissionData.colliderOffset.x + (collissionData.colliderSize.x * 0.5f);

        Vector2 wallPosition = new Vector2(rightEdge, wallCheck.localPosition.y);
        wallCheck.localPosition = wallPosition;

        Vector2 hardSurfacePosition = new Vector2(rightEdge, hardSurfaceCheck.localPosition.y);
        hardSurfaceCheck.localPosition = hardSurfacePosition;

        Vector2 ledgePosition = new Vector2(rightEdge, ledgeCheck.localPosition.y);
        ledgeCheck.localPosition = ledgePosition;
    }
}
