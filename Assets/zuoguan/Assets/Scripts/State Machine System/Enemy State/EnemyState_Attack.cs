using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Attack", fileName = "EnemyState_Attack")]
public class EnemyState_Attack : EnemyState
{
    [SerializeField] float startTime = 0.17f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] public float interval = 1.0f;
    [SerializeField] public float duration = 1f;
    [SerializeField] public String bullet = "Enemy1_Bullet";

    private float time = 0f;
    
    public override void Enter()
    {
        base.Enter();
        time = startTime;
    }

    public override void LogicUpdate()
    {
        
        
        if (StateDuration < duration)
        {
            return;
        }
        
        if (stateMachine.Contain(typeof(EnemyState_Idle)))
        {
            stateMachine.SwitchState(typeof(EnemyState_Idle));
            return;
        }
        
        if (enemy.transform.position.x - enemy.pos.x > enemy.rightRange || enemy.pos.x - enemy.transform.position.x > enemy.leftRange)
        {
            enemy.SetCdTime(1.0f);
            if (stateMachine.Contain(typeof(EnemyState_Run)))
            {
                stateMachine.SwitchState(typeof(EnemyState_Run));
                
            }
           
            
        }
        // stateMachine.SwitchState(typeof(EnemyState_Chase));
    }

    public override void PhysicUpdate()
    {
        enemy.SetVelocityX(0f);
        if (time < interval)
        {
            time += Time.deltaTime;
            return;
        }
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Traps/" + bullet));
        // Debug.Log(obj);
        obj.transform.position = enemy.transform.position + new Vector3(-enemy.transform.localScale.x * 1.5f, 0, 0);
        // Debug.Log(obj.transform.position);
        Bullet b = obj.GetComponent<Bullet>();
        // Debug.Log(-enemy.transform.localScale.x * 1.5f);
        b.bulletSpeed = new Vector2(-enemy.transform.localScale.x * bulletSpeed, 0);
        b.a = 0f;
        time = 0f;
        
    }
}
