using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedObj : MonoBehaviour
{
    public bool InAttackRange;
    private PlayerController playerController;


    private void Update()
    {
        if (InAttackRange && playerController != null && playerController.IsAttacking())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.transform.gameObject.tag);
        if (other.transform.gameObject.CompareTag("Weapon"))
        {
            InAttackRange = true;
        }
        playerController = other.transform.gameObject.GetComponentInParent<PlayerController>();
        // Debug.Log(playerController);
        // if ()
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.transform.gameObject.CompareTag("Weapon"))
        {
            InAttackRange = false;
        }
    }
    
}
