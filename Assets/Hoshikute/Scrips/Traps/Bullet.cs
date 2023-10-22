using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 bulletSpeed;

    public float a;

    private Rigidbody2D rb;

    private float duration;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.gravityScale = a;
        rb.velocity = bulletSpeed;
        duration = 0f;
    }

    private void FixedUpdate()
    {
        duration += Time.deltaTime;
        if (duration > 1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.name == this.gameObject.name)
        //     return;
        //伤害逻辑
        // Debug.Log(collision.gameObject);
        // Debug.Log(collision.gameObject.layer);
        
        // Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Weapon") && collision.gameObject.layer != 6 && collision.gameObject.name != "Bound")
        {
            Invoke("DestorySelf", 0.1f);
        }
        
        
    }

    private void DestorySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if (!collision.gameObject.CompareTag("Weapon") && collision.gameObject.layer != 6 && collision.gameObject.name != "Bound")
        {
            Invoke("DestorySelf", 0.1f);
        }
    }

   
        
    
}
