using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateId initialState;
    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public Animator animator;
    public Transform[] patrolPoints;
    public Vector3 targetLoc;
    [HideInInspector] public Transform player;

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

    public void Example(){
        stateMachine.ChangeState(AIStateId.Idle);
    }
}
