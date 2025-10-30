using UnityEngine;

public class GroundChecker : MonoBehaviour, IGroundChecker
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckOrigin;
    [SerializeField] private float checkWidth = 0.65f;
    [SerializeField] private float checkHeight = 0.075f;
    [SerializeField, Range(1, 10)] private int rayCount = 3;

    public bool IsGrounded => CheckGround();

    private bool CheckGround()
    {
        if (groundCheckOrigin == null) return false;

        float halfWidth = checkWidth / 2f;
        float spacing = rayCount > 1 ? checkWidth / (rayCount - 1) : 0f;
        bool grounded = false;

        for (int i = 0; i < rayCount; i++)
        {
            float xOffset = -halfWidth + spacing * i;
            Vector2 origin = (Vector2)groundCheckOrigin.position + Vector2.right * xOffset;

            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkHeight, groundLayer);
            if (hit.collider != null)
            {
                grounded = true;
                break;
            }
        }

        return grounded;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckOrigin == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;

        float halfWidth = checkWidth / 2f;
        float spacing = rayCount > 1 ? checkWidth / (rayCount - 1) : 0f;

        for (int i = 0; i < rayCount; i++)
        {
            float xOffset = -halfWidth + spacing * i;
            Vector3 origin = groundCheckOrigin.position + Vector3.right * xOffset;
            Gizmos.DrawLine(origin, origin + Vector3.down * checkHeight);
        }
    }
}
