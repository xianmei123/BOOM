
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Climb", fileName = "PlayerState_Climb")]
public class PlayerState_Climb : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(new Vector3(0.0f, 0.0f, 0.0f));
        
        player.SetUseGravity(0.0f);
        
    }

    public override void LogicUpdate()
    {
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }
        // if (input.StopJump || player.IsFalling)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Fall));
        // }
        //
        // if (input.Sprint)
        // {
        //     if (player.canFlySprint)
        //     {
        //         stateMachine.SwitchState(typeof(PlayerState_FlySprint));
        //     }
        // }

        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        
    }

    public override void PhysicUpdate()
    {
        
    }

    public override void Exit()
    {
        player.SetUseGravity(1.0f);
    }
}
