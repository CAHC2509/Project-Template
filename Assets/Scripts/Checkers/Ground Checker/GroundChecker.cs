using UnityEngine;

public class GroundChecker : MonoBehaviour, IGroundChecker
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckOrigin;
    [SerializeField] private float checkWidth = 0.75f;
    [SerializeField] private float checkHeight = 0.1f;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapBox(
            groundCheckOrigin.position,
            new Vector2(checkWidth, checkHeight),
            0f,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckOrigin == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheckOrigin.position, new Vector3(checkWidth, checkHeight, 0f));
    }
}
