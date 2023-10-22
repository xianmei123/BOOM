using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Death", fileName = "PlayerState_Death")]
public class PlayerState_Death : PlayerState
{
    
    public override void Enter()
    {
        base.Enter();
        player.SetInvincible();
    }

    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            player.Restart();
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        
    }

    public override void PhysicUpdate()
    {
        player.SetVelocity(new Vector3(0, 0, 0));
    }

    public override void Exit()
    {
        player.SetPlayer();
    }
}
