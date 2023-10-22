using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/JumpUp", fileName = "PlayerState_JumpUp")]
public class PlayerState_JumpUp : PlayerState
{
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool canSmallJump = true;
    [SerializeField] float deAcceleration = 3.0f;
    
    public override void Enter()
    {
        base.Enter();

        input.HasJumpInputBuffer = false;
        player.SetVelocityY(jumpForce);
        player.SetUseGravity(deAcceleration);
    }

    public override void LogicUpdate()
    {
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }
        if ((input.StopJump && canSmallJump) || player.IsFalling)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        else if (input.Sprint && player.FlySprintSkill)
        {
            if (player.canFlySprint)
            {
                stateMachine.SwitchState(typeof(PlayerState_FlySprint));
            }
        }
    }

    public override void PhysicUpdate()
    {
        player.Move(moveSpeed);
    }

    public override void Exit()
    {
        player.SetUseGravity(1.0f);
    }
}