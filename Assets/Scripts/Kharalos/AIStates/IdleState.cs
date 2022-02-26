using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    private float timer = 2f;
    public AIStateId GetId(){
        return AIStateId.Idle;
    }

    public void Enter(AIAgent agent){
        agent.navAgent.speed = 1f;
    }
    public void Update(AIAgent agent){
        timer -= Time.deltaTime;
        if(agent.interactionPoints.Length > 0){
            if(timer < 0.0f) agent.stateMachine.ChangeState(AIStateId.Interaction); 
        }
        else if(agent.patrolPoints.Length > 0){
            if(timer < 0.0f) agent.stateMachine.ChangeState(AIStateId.Patrol); 
        }
    }
    public void Exit(AIAgent agent){
        timer = 0;
    }
}
