using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FlySprint", fileName = "PlayerState_FlySprint")]
public class PlayerState_FlySprint : PlayerState
{
    [SerializeField] float SprintTime = 0.5f;
    [SerializeField] float SprintDistance = 4.0f;
    [SerializeField] private float imageDistance = 0.4f;
    private float currentSpeedX;
    private float currentSpeedY;
    private float lastImagePos;
    
    public override void Enter()
    {
        base.Enter();
        player.EnterFlySprintCD();
        player.SetInvincible();
        currentSpeedX = player.MoveSpeedX;
        currentSpeedY = player.MoveSpeedY;
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

        stateMachine.SwitchState(typeof(PlayerState_Fall));
        
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(0);
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
        player.SetVelocityY(currentSpeedY);
    }
}