using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Death", fileName = "EnemyState_Death")]
public class EnemyState_Death : EnemyState
{
    [SerializeField] float deceleration = 5f;

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
       
    }

    public override void PhysicUpdate()
    {
        
        
    }
}
