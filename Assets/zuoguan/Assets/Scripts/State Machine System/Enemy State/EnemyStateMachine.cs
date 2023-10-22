using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] EnemyState[] states;
    [SerializeField] EnemyState initialState;
    Animator animator;

    EnemyController enemy;
    

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
     
        
        enemy = GetComponent<EnemyController>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach (EnemyState state in states)
        {
            state.Initialize(animator, enemy, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        SwitchOn(initialState);
    }

    public bool Contain(System.Type type)
    {
        return stateTable.ContainsKey(type);
    }
}
