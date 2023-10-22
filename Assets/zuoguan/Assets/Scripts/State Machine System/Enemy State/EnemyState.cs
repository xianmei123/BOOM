using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : ScriptableObject, IState
{
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

    float stateStartTime;

    int stateHash;

    protected float currentSpeed;

    protected Animator animator;

    protected EnemyController enemy;
    protected EnemyStateMachine stateMachine;

    // protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected bool IsAnimationFinished => StateDuration >= 0;

    protected float StateDuration => Time.time - stateStartTime;

    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public void Initialize(Animator animator, EnemyController enemy, EnemyStateMachine stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        // Debug.Log(stateHash);
        animator.CrossFade(stateHash, transitionDuration);
        stateStartTime = Time.time;
        // Debug.Log(stateName);
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
}
