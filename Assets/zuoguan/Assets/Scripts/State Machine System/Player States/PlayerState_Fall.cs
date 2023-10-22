using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float moveSpeed = 5f;

    public override void LogicUpdate()
    {
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }
        if (player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }

        else if (player.CanClimb && player.ClimbSkill)
        {
            stateMachine.SwitchState(typeof(PlayerState_Climb));
            return;
        }
        
        else if (input.Sprint && player.FlySprintSkill)
        {
            if (player.canFlySprint)
            {
                stateMachine.SwitchState(typeof(PlayerState_FlySprint));
            }
        }
        else if (input.Jump)
        {
            input.SetJumpInputBufferTimer();
        }
    }

    public override void PhysicUpdate()
    {
        player.Move(moveSpeed);
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
    }
}