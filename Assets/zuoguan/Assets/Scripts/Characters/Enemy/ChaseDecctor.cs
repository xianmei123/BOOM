using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseDecctor : MonoBehaviour
{
    [SerializeField] float detectionRadius = 3f;
    [SerializeField] LayerMask layerMask;
    Collider[] colliders = new Collider[1];
    public float dir = 1f;

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - new Vector3(detectionRadius, 0, 0), transform.position + new Vector3(detectionRadius, 0, 0));
        // Gizmos.DrawWire(transform.position, detectionRadius);
    }

    public bool InRange()
    {
        RaycastHit2D hit;
        
        hit = Physics2D.Raycast(transform.position - new Vector3(detectionRadius, 0, 0), Vector2.right,
            2 * detectionRadius, layerMask);
        
        
        if (hit.collider != null)
        {
            dir = hit.collider.transform.position.x - transform.position.x > 0 ? 1f : -1f;
            return true;
        }

        
        
        return false;
    }
}
