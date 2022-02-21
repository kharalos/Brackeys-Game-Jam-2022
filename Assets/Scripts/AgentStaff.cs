using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentStaff : AIAgent
{
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new RunningState());
        stateMachine.RegisterState(new ScaredState());
        stateMachine.RegisterState(new ChaseState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }

}
