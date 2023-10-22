using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Run", fileName = "EnemyState_Run")]
public class EnemyState_Run : EnemyState
{

    [SerializeField] float runSpeed = 4f;
    [SerializeField] float leftRange = 4f;
    [SerializeField] float rightRange = 4f;
    private float dir = 1;
    private float delta = 0.0f;
    
    public override void Enter()
    {
        base.Enter();
        currentSpeed = runSpeed;
        enemy.SetRange(leftRange, rightRange);
        delta = 0f;
    }

    public override void LogicUpdate()
    {
   
        // Debug.Log(enemy.IsRightGrounded + " "+ enemy.IsLeftGrounded);
        
        if (enemy.transform.position.x - enemy.pos.x < -leftRange || !enemy.IsRightGrounded || enemy.HasRightWall)
        {
            enemy.runDir = 1.0f;
            return;
        }
        else if (enemy.transform.position.x - enemy.pos.x > rightRange ||!enemy.IsLeftGrounded || enemy.HasLeftWall )
        {
            enemy.runDir = -1.0f;
            return;
        }
        
        
        if (delta < enemy.getCdTime())
        {
            delta += Time.deltaTime;
            return;
        }
        
        
        
        if (stateMachine.Contain(typeof(EnemyState_Chase)) && enemy.InRange)
        {
            stateMachine.SwitchState(typeof(EnemyState_Chase));
            return;
        }
        

       
     
    }

    public override void PhysicUpdate()
    {
        enemy.Move(currentSpeed, enemy.runDir);
    }
}
