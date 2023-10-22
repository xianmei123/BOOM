using System;
using UnityEngine;

public class RightGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    Collider[] colliders = new Collider[1];

    public bool IsGrounded => Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null;
    public bool HasWall => Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, wallLayer) != null;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // private void Update()
    // {
    //     Debug.Log((Vector2)transform.position);
    //     Debug.Log(Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null);
    //     // Debug.Log(Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, groundLayer) != 0);
    // }
}