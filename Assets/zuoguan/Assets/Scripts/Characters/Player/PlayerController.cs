using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float flySprintCD = 5f;
    [SerializeField] private float SprintCD = 5f;
    [SerializeField] private bool invincible = false;
  
    [SerializeField] public List<Vector3> pos;
    private float flySprintTime = 0.0f;
    private float sprintTime = 0.0f;

    private PlayerGroundDetector groundDetector;
    private PlayerClimbDetector ClimbDetector;
    private PlayerInput input;

    private Rigidbody2D rigidBody;

    private Collider2D otherCollider;

    private CapsuleCollider2D playerCollider;
    private bool canInteraction = false;

    private bool isAttacking = false;

    public AudioSource VoicePlayer { get; private set; }

    public bool CanAirJump { get; set; }
    public bool Victory { get; private set; }


    public bool canSprint = true;

    public bool canFlySprint = true;


    private Vector3 InitialPos;

    public bool CanClimb => ClimbDetector.CanClimb;
    public bool IsGrounded => groundDetector.IsGrounded;
    public bool IsFalling => rigidBody.velocity.y < 0f && !IsGrounded;

    public float MoveSpeedX => Mathf.Abs(rigidBody.velocity.x);
    public float MoveSpeedY => rigidBody.velocity.y;

    private GameObject InteractionObject;

    private MovingPlatform movingPlatform;
    public bool InMovingPlatform = false;


    public bool SprintSkill { get; set; }
    public bool FlySprintSkill { get; set; }
    public bool ClimbSkill { get; set; }
    public bool AttackSkill { get; set; }

    private int skillType = 0;

    private bool IsDeath = false;

    public Vector2 distance;

    

    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        ClimbDetector = GetComponentInChildren<PlayerClimbDetector>();
        input = GetComponent<PlayerInput>();
        if (input == null)
        {
            Debug.Log("false");
        }

        rigidBody = GetComponent<Rigidbody2D>();
        VoicePlayer = GetComponentInChildren<AudioSource>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("Player");


        // Debug.Log(SkillData.Instance.SprintSkill + " " + SkillData.Instance.FlySprintSkill + " " + SkillData.Instance.ClimbSkill + " " + SkillData.Instance.AttackSkill);
  
        

    }


    private void Start()
    {
        input.EnableGameplayInputs();
        SprintSkill = SkillData.Instance.SprintSkill;
        FlySprintSkill = SkillData.Instance.FlySprintSkill;
        ClimbSkill = SkillData.Instance.ClimbSkill;
        AttackSkill = SkillData.Instance.AttackSkill;
       
        transform.position = pos[SkillData.Instance.index];
        InitialPos = transform.position;
        // Dialogue
        dialogueEventChannel.OnDialogueStateChanged -= OnDialogueStateChanged;
        dialogueEventChannel.OnDialogueStateChanged += OnDialogueStateChanged;
        
        
        
    }

    private void Update()
    {
        if (sprintTime < SprintCD)
        {
            sprintTime += Time.deltaTime;
        }
        else
        {
            canSprint = true;
        }

        if (flySprintTime < flySprintCD)
        {
            flySprintTime += Time.deltaTime;
        }
        else
        {
            canFlySprint = true;
        }

        if (InMovingPlatform)
        {
            distance = movingPlatform.GetOffset();
        }

        if (input.Interaction && skillType > 0)
        {
            switch (skillType)
            {
                case 1:
                    SprintSkill = true;

                    break;
                case 2:
                    FlySprintSkill = true;
                    // SkillData.Instance.FlySprintSkill = true;
                    break;
                case 3:
                    ClimbSkill = true;
                    // SkillData.Instance.ClimbSkill = true;
                    break;
                case 4:
                    AttackSkill = true;
                    // SkillData.Instance.AttackSkill = true;
                    break;
            }
            // Debug.Log(SprintSkill + " " + FlySprintSkill + " " + ClimbSkill + " " + AttackSkill);
        }
    }


    private void FixedUpdate()
    {
        if (InMovingPlatform)
        {
            transform.Translate(distance);
        }
    }

    public void EnterSprintCD()
    {
        canSprint = false;
        sprintTime = 0.0f;
    }

    public void EnterFlySprintCD()
    {
        canFlySprint = false;
        flySprintTime = 0.0f;
    }

    public void Move(float speed)
    {
        if (input.Move)
        {
            transform.localScale = new Vector3(input.AxisX, 1f, 1f);
        }

        SetVelocityX(speed * input.AxisX);
    }


    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    public void SetUseGravity(float value)
    {
        // rigidBody.useGravity = value;
        rigidBody.gravityScale = value;
    }

    public Vector3 GetVelocity()
    {
        return rigidBody.velocity;
    }

    public Vector2 GetPlayerSize()
    {
        return playerCollider.size;
    }

    public void SetPlayerSize(Vector2 size)
    {
        playerCollider.size = size;
    }

    public Vector2 GetPlayerOffset()
    {
        return playerCollider.offset;
    }

    public void SetPlayerOffset(Vector2 offset)
    {
        playerCollider.offset = offset;
    }

    public void SetInvincible()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
    }

    public void SetPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void InPlatform()
    {
        groundDetector.InPlatform();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.transform.name);
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                Dead();
            }
            else if (collision.transform.CompareTag("Laser"))
            {
                Dead();
            }
            else if (collision.transform.CompareTag("Bullet"))
            {
                Dead();
            }
        }

        // Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("MovingPlatform"))
        {
            InMovingPlatform = true;

            movingPlatform = collision.transform.gameObject.GetComponent<MovingPlatform>();
            distance = movingPlatform.GetOffset();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("MovingPlatform"))
        {
            InMovingPlatform = false;
        }
    }

    public void Dead()
    {
        if (!invincible)
        {
            // Destroy(this.gameObject);
            // transform.position = InitialPos;
            SprintSkill = SkillData.Instance.SprintSkill;
            FlySprintSkill = SkillData.Instance.FlySprintSkill;
            ClimbSkill = SkillData.Instance.ClimbSkill;
            AttackSkill = SkillData.Instance.AttackSkill;
        }

        IsDeath = true;
    }

    public bool PlayerDeath()
    {
        return IsDeath;
    }

    public void Restart()
    {
        IsDeath = false;
        transform.position = InitialPos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Debug.Log(other.transform.name);
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            otherCollider = other;
            if (other.transform.CompareTag("Bullet"))
            {
                Dead();
            }
        }

        if (other.transform.CompareTag("SceneTrigger"))
        {
            SkillData.Instance.SprintSkill = SprintSkill;
            SkillData.Instance.FlySprintSkill = FlySprintSkill;
            SkillData.Instance.ClimbSkill = ClimbSkill;
            SkillData.Instance.AttackSkill = AttackSkill;
        }
        else if (other.transform.CompareTag("Interaction_Skill_Sprint"))
        {
            // Debug.Log("Enter Interaction_Skill_Sprint");
            skillType = 1;
        }
        else if (other.transform.CompareTag("Interaction_Skill_FlySprint"))
        {
            // Debug.Log("Enter Interaction_Skill_FlySprint");
            skillType = 2;
        }
        else if (other.transform.CompareTag("Interaction_Skill_Climb"))
        {
            // Debug.Log("Enter Interaction_Skill_Climb");
            skillType = 3;
        }
        else if (other.transform.CompareTag("Interaction_Skill_Attack"))
        {
            // Debug.Log("Enter Interaction_Skill_Attack");
            skillType = 4;
        }
        else if (other.transform.CompareTag("Stuff"))
        {
            SkillData.Instance.IDs.Add(int.Parse(other.transform.gameObject.name));
            Destroy(other.transform.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag.Contains("Interaction_Skill"))
        {
            // Debug.Log("out ");
            skillType = 0;
        }
    }


    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         otherCollider = other;
    //         if (other.gameObject.CompareTag("trap")) 
    //         {
    //             Debug.Log("trap");
    //         }
    //         else if (other.gameObject.CompareTag("enemy")) 
    //         {
    //             Debug.Log("enemy");
    //         }
    //         else
    //         {
    //             canInteraction = true;
    //             Debug.Log("can w");
    //         }
    //         
    //         
    //     }
    //     
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     canInteraction = false;
    // }
    //
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     
    // }
    //
    // void Interaction()
    // {
    //     if (canInteraction && input.Interaction)
    //     {
    //         Debug.Log("Start Interaction");
    //     }
    // }
    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void setAttacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }

    #region Dialogue

    [SerializeField] private DialogueEventChannel dialogueEventChannel;

    private void OnDialogueStateChanged(DialogueState dialogueState)
    {
        if (dialogueState == DialogueManager.Instance.DialogueStateInDialogue)
        {
            input.DisableGameplayInputs();
            Time.timeScale = 0f;
        }
        else
        {
            input.EnableGameplayInputs();
            Time.timeScale = 1f;
        }
    }

    #endregion
}