using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState
{
    private int patrolNumber = 0;
    private int patrolLength;
    public AIStateId GetId(){
        return AIStateId.Patrol;
    }

    public void Enter(AIAgent agent){
        agent.navAgent.speed = 1f;
        patrolNumber = 0;
        patrolLength = agent.patrolPoints.Length;
    }
    private void PatrolCycle(){
        patrolNumber++;
        if(patrolNumber >= patrolLength){
            patrolNumber = 0;
        }
    }
    public void Update(AIAgent agent){
        agent.navAgent.SetDestination(agent.patrolPoints[patrolNumber].position);
        if(Vector3.Distance(agent.transform.position, agent.patrolPoints[patrolNumber].position) < 2f && patrolLength > 1)
            PatrolCycle();
    }
    public void Exit(AIAgent agent){
        agent.navAgent.speed = 3f;
    }
}
