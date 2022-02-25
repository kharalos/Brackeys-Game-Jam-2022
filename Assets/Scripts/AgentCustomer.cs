using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCustomer : AIAgent
{
    private Vector3 pos;
    void Start()
    {
        pos = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new RunningState());
        stateMachine.RegisterState(new ScaredState());
        stateMachine.ChangeState(initialState);
    }   
    void Update()
    {
        stateMachine.Update();
        animator.SetFloat("speed", ((transform.position - pos) / Time.deltaTime).magnitude);
        pos = transform.position;
    }
    public void Scared()
    {
        targetLoc = GetRandomCover(FindObjectOfType<CoverPlacesHandler>().coverPlaces).position;
        stateMachine.ChangeState(AIStateId.Scared); 
    }

    Transform GetRandomCover(Transform[] objects)
    {
        return objects[Random.Range(0, objects.Length)];
    }

}
