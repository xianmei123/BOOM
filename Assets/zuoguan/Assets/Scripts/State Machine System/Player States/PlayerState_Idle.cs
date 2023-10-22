using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField] float deceleration = 5f;

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
        if (input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
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
        else if (input.Attack && player.AttackSkill)
        {
            stateMachine.SwitchState(typeof(PlayerState_Attack));
        }
        

        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
      
        player.SetVelocityX(currentSpeed * player.transform.localScale.x);
        
    }
}