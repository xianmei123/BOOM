using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "PlayerState_Land")]
public class PlayerState_Land : PlayerState
{
    [SerializeField] float stiffTime = 0.2f;
    [SerializeField] ParticleSystem landVFX;

    public override void Enter()
    {
        base.Enter();

        // player.SetVelocity(Vector3.zero);
        player.SetVelocityY(0);
        // Instantiate(landVFX, player.transform.position, Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        
       
        if (player.PlayerDeath())
        {
            stateMachine.SwitchState(typeof(PlayerState_Death));
            return;
        }

        if (StateDuration < stiffTime)
        {
            return;
        }
        
        if (input.Jump || input.HasJumpInputBuffer)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
            
        }
        else if (input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
        else if (input.Squat)
        {
            stateMachine.SwitchState(typeof(PlayerState_Squat));
         
        }
        else 
        {
            // Debug.Log("yes  ");
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}