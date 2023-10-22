using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    Collider[] colliders = new Collider[1];

    public bool CanClimb => Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (CanClimb)
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // private void Update()
    // {
    //     Debug.Log((Vector2)transform.position);
    //     Debug.Log(Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null);
    //     // Debug.Log(Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, groundLayer) != 0);
    // }
}
