using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Squat", fileName = "PlayerState_Squat")]
public class PlayerState_Squat : PlayerState
{
    private Vector2 playerColliderSize;
    private Vector2 playerColliderOffset;
    [SerializeField] float scale = 0.5f;
    public override void Enter()
    {
        base.Enter();
        player.SetInvincible();
        playerColliderSize = player.GetPlayerSize();
        playerColliderOffset = player.GetPlayerOffset();
        player.SetVelocity(new Vector3(0.0f, 0.0f, 0.0f));
       
        player.SetPlayerSize(new Vector2(playerColliderSize.x, playerColliderSize.y * scale));
        if (playerColliderSize.y * scale > 0.5f)
        {
            player.SetPlayerOffset(new Vector2(playerColliderOffset.x, -playerColliderSize.y * 0.5f * (1 - scale)));
        }
        else
        {
            player.SetPlayerOffset(new Vector2(playerColliderOffset.x, -0.25f));
        }
        
        
    }

    public override void LogicUpdate()
    {
        if (!input.Squat)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else if (input.Jump)
        {
            player.InPlatform();
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
       
    }

    public override void Exit()
    {
        player.SetPlayer();
        player.SetPlayerSize(playerColliderSize);
        player.SetPlayerOffset(playerColliderOffset);
    }
}
