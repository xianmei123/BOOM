using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Sprint", fileName = "PlayerState_Sprint")]
public class PlayerState_Sprint : PlayerState
{
    [SerializeField] float SprintTime = 0.5f;
    [SerializeField] float SprintDistance = 4.0f;
    [SerializeField] private float imageDistance = 0.4f;
    private float currentSpeedX;
    private float lastImagePos;
    
    public override void Enter()
    {
        base.Enter();
        player.EnterSprintCD();
        player.SetInvincible();
        currentSpeedX = player.MoveSpeedX;
        player.SetVelocityX(SprintDistance / SprintTime * player.transform.localScale.x);
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImagePos = player.transform.position.x;
    }

    public override void LogicUpdate()
    {
        
        
      
        
        
        if (StateDuration < SprintTime)
        {
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
        else
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        
    }

    public override void PhysicUpdate()
    {
        if (Mathf.Abs(player.transform.position.x - lastImagePos) > imageDistance)
        {
            
            PlayerAfterImagePool.Instance.GetFromPool();
            
            lastImagePos = player.transform.position.x;
        }
    }

    public override void Exit()
    {
        player.SetPlayer();
        player.SetVelocityX(currentSpeedX);
        
    }
}
