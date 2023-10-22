using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    Rigidbody2D rigidBody;

    private ChaseDecctor chaseDetector;
    
    public float MoveSpeedX => Mathf.Abs(rigidBody.velocity.x);

    private LeftGroundDetector leftGroundDetector;
    private RightGroundDetector rightGroundDetector;
    private PlayerController playerController;
    
    public Vector3 pos;

    public float leftRange;
    public float rightRange;
    public bool InRange;
    public float dir => chaseDetector.dir;
    private float cdTime = 0f;
    public float runDir;
    
    public bool IsLeftGrounded;
    public bool IsRightGrounded;
    
    public bool HasLeftWall;
    
    public bool HasRightWall;

    public bool InAttackRange;
    
    private void Awake()
    {
        leftGroundDetector = GetComponentInChildren<LeftGroundDetector>();
        rightGroundDetector = GetComponentInChildren<RightGroundDetector>();
        chaseDetector = GetComponentInChildren<ChaseDecctor>();
        rigidBody = GetComponent<Rigidbody2D>();
        pos = transform.position;
        
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        InRange = chaseDetector.InRange();
        if (transform.localScale.x == -1f)
        {
            IsLeftGrounded = leftGroundDetector.IsGrounded;
            IsRightGrounded = rightGroundDetector.IsGrounded;
            HasLeftWall = leftGroundDetector.HasWall;
            HasRightWall = rightGroundDetector.HasWall;
        }
        else
        {
            IsLeftGrounded = rightGroundDetector.IsGrounded;
            IsRightGrounded = leftGroundDetector.IsGrounded;
            HasLeftWall = rightGroundDetector.HasWall;
            HasRightWall = leftGroundDetector.HasWall;
        }
        
        if (InAttackRange && playerController != null && playerController.IsAttacking())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.name == "Player")
        // {
        //     Destroy(other.gameObject);
        // }
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

    public void Move(float speed, float dir)
    {
        transform.localScale = new Vector3(-dir, 1f, 1f);
        rigidBody.velocity = new Vector3(dir * speed, rigidBody.velocity.y);
    }
    
    
    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y);
    }

    public void SetRange(float leftRange, float rightRange)
    {
        this.leftRange = leftRange;
        this.rightRange = rightRange;
    }

    public void SetCdTime(float time)
    {
        cdTime = time;
    }

    public double getCdTime()
    {
        return cdTime;
    }

    public void SetRunDir(float runDir)
    {
        throw new NotImplementedException();
    }
}
