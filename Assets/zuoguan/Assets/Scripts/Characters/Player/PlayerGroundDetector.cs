using System;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    Collider[] colliders = new Collider[1];

    public bool IsGrounded => Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
    }

    public bool InPlatform()
    {
        
        RaycastHit2D check = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 2 * detectionRadius, groundLayer);
        if (check.collider == null)
        {
            return false;
        }
        
        if (check.collider.CompareTag("Platform"))
        {
            GameObject platform = check.collider.gameObject;
            platform.GetComponent<PlatformEffector2D>().colliderMask -= (1 << LayerMask.NameToLayer("Player")); 
            platform.GetComponent<PlatformEffector2D>().colliderMask -= (1 << LayerMask.NameToLayer("Invincible"));
            platform.layer = LayerMask.NameToLayer("Default");
            return true;
            // this.GetComponent<PlatformEffector2D>().colliderMask -= (1 << GameManager.instance.PMC.gameObject.layer); 
        }

        return false;
    }

    // private void Update()
    // {
    //     Debug.Log((Vector2)transform.position);
    //     Debug.Log(Physics2D.OverlapCircle((Vector2)transform.position, detectionRadius, groundLayer) != null);
    //     // Debug.Log(Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, groundLayer) != 0);
    // }
}