using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float acceleration = 5f;

    public override void Enter()
    {
        base.Enter();

        currentSpeed = player.MoveSpeedX;
    }

    public override void LogicUpdate()
    {
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }
        
        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        
        else if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        
        else if (input.Sprint && player.SprintSkill)
        {
            if (player.canSprint)
            {
                stateMachine.SwitchState(typeof(PlayerState_Sprint));
            }
            
        }
        
        else if (input.Squat)
        {
            stateMachine.SwitchState(typeof(PlayerState_Squat));
        }
        
        else if (!player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        } 
        else if (input.Attack && player.AttackSkill)
        {
            
            stateMachine.SwitchState(typeof(PlayerState_Attack));
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        
        if (player.InMovingPlatform)
        {
            player.Move(currentSpeed);
            // player.transform.Translate(player.distance);
            // player.SetVelocityY(player.platformSpeed.y > 0 ? player.platformSpeed.y : player.platformSpeed.y*2);
        }
        else
        {
            player.Move(currentSpeed);
        }
        
        
    }
}