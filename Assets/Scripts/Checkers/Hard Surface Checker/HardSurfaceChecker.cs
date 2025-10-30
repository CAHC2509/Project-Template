using UnityEngine;

public class HardSurfaceChecker : MonoBehaviour, IHardSurfaceChecker
{
    [SerializeField] private LayerMask hardSurfaceLayer;
    [SerializeField] private Transform hardSurfaceCheckOrigin;
    [SerializeField] private float checkDistance = 0.55f;

    public bool IsTouchingHardSurface => CheckHardSurface();

    private bool CheckHardSurface()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(hardSurfaceCheckOrigin.position, direction, checkDistance, hardSurfaceLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (hardSurfaceCheckOrigin == null) return;

        Gizmos.color = IsTouchingHardSurface ? Color.green : Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(hardSurfaceCheckOrigin.position, hardSurfaceCheckOrigin.position + (Vector3)direction * checkDistance);
    }
}
