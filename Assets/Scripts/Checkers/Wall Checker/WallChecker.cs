using UnityEngine;

public class WallChecker : MonoBehaviour, IWallChecker
{
    [SerializeField] private LayerMask wallsLayer;
    [SerializeField] private Transform wallCheckOrigin;
    [SerializeField] private float checkDistance = 0.2f;

    public bool IsTouchingWall { get; private set; }

    private void Update()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckOrigin.position, direction, checkDistance, wallsLayer);
        IsTouchingWall = hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (wallCheckOrigin == null) return;

        Gizmos.color = IsTouchingWall ? Color.green : Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(wallCheckOrigin.position, wallCheckOrigin.position + (Vector3)direction * checkDistance);
    }
}
