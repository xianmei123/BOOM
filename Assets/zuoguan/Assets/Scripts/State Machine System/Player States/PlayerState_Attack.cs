using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Attack", fileName = "PlayerState_Attack")]
public class PlayerState_Attack : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        
    }
    
    public override void LogicUpdate()
    {

        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }
        
        if (StateDuration > 0.3f)
        {
            player.setAttacking(true);
        }
        
        if (StateDuration > 0.50f)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        
        
    }

    public override void PhysicUpdate()
    {

        player.SetVelocityX(0);

    }

    public override void Exit()
    {
        player.setAttacking(false);
    }
}
