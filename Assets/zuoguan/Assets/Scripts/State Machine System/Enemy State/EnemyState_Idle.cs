using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Idle", fileName = "EnemyState_Idle")]
public class EnemyState_Idle : EnemyState
{

    public override void Enter()
    {
        base.Enter();
        enemy.SetRange(0, 0);
    }

    public override void LogicUpdate()
    {
        if (stateMachine.Contain(typeof(EnemyState_Chase)) && enemy.InRange)
        {
            stateMachine.SwitchState(typeof(EnemyState_Chase));
            
            return;
        }
        else if (stateMachine.Contain(typeof(EnemyState_Attack)) && enemy.InRange)
        {
            // Debug.Log(enemy.dir);
            enemy.transform.localScale =
                new Vector3(-enemy.dir, enemy.transform.localScale.y, enemy.transform.localScale.z);
            stateMachine.SwitchState(typeof(EnemyState_Attack));
            return;
        }
    }

    public override void PhysicUpdate()
    {
        
        
    }
}