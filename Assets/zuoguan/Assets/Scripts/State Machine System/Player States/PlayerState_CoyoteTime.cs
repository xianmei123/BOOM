using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float runSpeed = 5f;
    public override void Enter()
    {
        base.Enter();
        player.SetUseGravity(0.0f);
        
    }

    public override void LogicUpdate()
    {
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }

        if (input.Jump)
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

        if (StateDuration > coyoteTime || !input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        player.Move(runSpeed);
    }

    public override void Exit()
    {
        player.SetUseGravity(1.0f);
    }
}
