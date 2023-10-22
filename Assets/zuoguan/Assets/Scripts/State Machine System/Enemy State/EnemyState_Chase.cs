using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Chase", fileName = "EnemyState_Chase")]
public class EnemyState_Chase : EnemyState
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float extRange = 3f;
    [SerializeField] float cdTime = 2f;
    
    private float dir = 1;
    private float delta = 0.0f;
    
    public override void Enter()
    {
        base.Enter();
        currentSpeed = runSpeed;
    }

    public override void LogicUpdate()
    {

        // Debug.Log("test");
        
        // Debug.Log(enemy.pos.x - enemy.transform.position.x > enemy.leftRange);
        
        if (enemy.transform.position.x - enemy.pos.x > enemy.rightRange + extRange || enemy.pos.x - enemy.transform.position.x > enemy.leftRange + extRange)
        {
            enemy.SetCdTime(cdTime);
            if (stateMachine.Contain(typeof(EnemyState_Attack)))
            {
                stateMachine.SwitchState(typeof(EnemyState_Attack));
            }
            else if (stateMachine.Contain(typeof(EnemyState_Run)))
            {
                stateMachine.SwitchState(typeof(EnemyState_Run));
            }
            
        }

        if (!enemy.IsLeftGrounded || !enemy.IsRightGrounded || enemy.HasLeftWall || enemy.HasRightWall)
        {
            
            
            stateMachine.SwitchState(typeof(EnemyState_Run));
        }
        
       
     
    }

    public override void PhysicUpdate()
    {
        if (enemy.transform.position.x - enemy.pos.x > enemy.rightRange + extRange || enemy.pos.x - enemy.transform.position.x > enemy.leftRange + extRange)
        {
            return;
        }
        enemy.Move(currentSpeed, enemy.dir);
    }

    public override void Exit()
    {
        
    }
}
